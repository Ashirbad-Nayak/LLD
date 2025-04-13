using System;
using BlinkItLLD.Domain;
using BlinkItLLD.Domain.Enums;

namespace BlinkItLLD
{
    public class CartManager
    {
        private static readonly Object _lock = new Object();
        private static CartManager _instance;
        private Dictionary<int, Cart> _carts = new Dictionary<int, Cart>(); //map if userId to cart
        private StoreManager _storeManager;
        private CartManager()
        {
            // Initialize properties and collections here
            _storeManager = StoreManager.Instance;
        }
        public static CartManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CartManager();
                    }
                    return _instance;
                }
            }
        }
        
        // Add methods and properties to manage the cart here
        public void AddToCart(int userId, int storeId, int itemId, int quantity)
        {

            Store store = _storeManager.GetStore(storeId);
            
            // Check if the store exists
            if (store == null)
            {
                Console.WriteLine("Store not found.");
                return;
            }

            // Check if the item exists in the store
            if (!store.Items.ContainsKey(itemId))
            {
                Console.WriteLine("Item not available in the store.");
                return;
            }

            // Check if any cart exists for the user
            // If not, create a new cart for the user
            if (!_carts.ContainsKey(userId))
            {
                _carts[userId] = new Cart(userId, storeId);
            }


            Item item = store.GetItem(itemId);
            
            //check if item is available in the store
            if (store.Items[itemId].Quantity < quantity)
            {
                Console.WriteLine("Not enough quantity available in the store.");
                return;
            }

            //create a clone of the item
            //add item to cart
            _carts[userId].AddItem(item, quantity);

            //update the store item quantity
            UpdateStoreItemQuantity(storeId, itemId, quantity, OperationTypeEnum.REMOVE);

        }
        public void RemoveFromCart(int userId, int storeId, int itemId, int quantity)
        {
            if (!_carts.ContainsKey(userId))
            {
                Console.WriteLine("Cart not found for the user.");
                return;
            }

            // Check if the item exists in the cart
            if (!_carts[userId].items.ContainsKey(itemId))
            {
                Console.WriteLine("Item not found in the cart.");
                return;
            }
            // Check if the item quantity in the cart is less than the quantity to be removed
            
            if (_carts[userId].items[itemId].Quantity < quantity)
            {
                Console.WriteLine("Not enough quantity in the cart to remove.");
                return;
            }
            //remove item from cart
            _carts[userId].RemoveItem(itemId, quantity);


            //update the store item quantity
            UpdateStoreItemQuantity(storeId, itemId, quantity, OperationTypeEnum.ADD);

            
        }
        public void ClearCart(int userId)
        {
            if (_carts.ContainsKey(userId))
            {
                _carts[userId].ClearCart();
            }
            else
            {
                Console.WriteLine("Cart not found for the user.");
            }
        }

        //restock
        public void Restock(int userId){
            // Restore cart items to the store
                Cart cart = _carts[userId];
                foreach (var item in cart.items)
                {
                    // Update the store item quantity
                    UpdateStoreItemQuantity(cart.StoreId, item.Value.Id, item.Value.Quantity, OperationTypeEnum.ADD);
                }
                ClearCart(userId);
        }

        //updatecart based on payment success/failure
        //clearcart on 
        public void UpdateCartOnPayment(int userId, bool isSuccess)
        {
            if (isSuccess)
            {
                Console.WriteLine("Resetting cart after successful payment.");
                ClearCart(userId);
            }
            else
            {
                Console.WriteLine("Payment failed. Restoring cart items.");
                Restock(userId);
            }
        }


        public void DisplayCart(int userId)
        {
            if (_carts.ContainsKey(userId))
            {
                _carts[userId].DisplayCart();
            }
            else
            {
                Console.WriteLine("Cart not found for the user.");
            }
        }
        public decimal GetTotalPrice(int userId)
        {
            if (_carts.ContainsKey(userId))
            {
                return _carts[userId].GetTotalPrice();
            }
            Console.WriteLine("Cart not found for the user.");
            return 0;
        }

        private void UpdateStoreItemQuantity(int storeId, int itemId, int quantity, OperationTypeEnum operationType)
        {
            if (_storeManager.GetStore(storeId).Items.ContainsKey(itemId))
            {
                _storeManager.GetStore(storeId).UpdateItemQuantity(itemId, quantity, operationType);
                Console.WriteLine("Store item quantity updated successfully.");
                
            }
            else
            {
                Console.WriteLine("Item not found in the store.");
            }
        }
    }
}