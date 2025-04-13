

using BlinkItLLD.Domain;
using BlinkItLLD.OrderManagementSystem;
using BlinkItLLD.StoreFinderStrategy;

namespace BlinkItLLD
{
    
    public class OrderProcessor
    {
        public StoreManager storeManager;
        private CartManager cartManager;
        private SearchManager searchManager;
        private PaymentManager paymentManager;
        private IStoreFinderStrategy storeFinderStrategy;
        private static readonly Object _lock = new Object();
        private static OrderProcessor _instance;
        private OrderProcessor()
        {
            // Initialize properties and collections here
            storeManager = StoreManager.Instance;
            cartManager = CartManager.Instance;
            searchManager = SearchManager.Instance;
            paymentManager = PaymentManager.Instance;
            storeFinderStrategy = new NearestStoreStrategy(); //hardcoded store finding strategy//can be based on user preference/various other factors
        }
        public static OrderProcessor Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new OrderProcessor();
                    }
                    return _instance;
                }
            }
        }

        public Store FindAStore(Location location, string itemName = null)
        {
            // Use the strategy to find a store
            List<Store> stores = storeManager.GetAllStores();
            return storeFinderStrategy.FindStore(location, stores, itemName);
        }

        //getstore items or view entire store
        public void ViewStoreItems(int storeId)
        {
            Store store = storeManager.GetStore(storeId);
            if (store != null)
            {
                Console.WriteLine($"Items in Store {storeId}:");
                store.DisplayItems();
            }
            else
            {
                Console.WriteLine("Store not found.");
            }
        }

        public void SearchItem(int storeId, string itemName)
        {
            // Use the search manager to find items in the store
            List<Item> items = searchManager.SearchItems(storeId, itemName);
            if (items.Count > 0)
            {
                Console.WriteLine($"Items found in Store {storeId}:");
                foreach (var item in items)
                {
                    Console.WriteLine($"Item ID: {item.Id}, Name: {item.Name}, Price: {item.Price}, Quantity: {item.Quantity}");
                }
            }
            else
            {
                Console.WriteLine($"No items found with the given name {itemName}.");
            }
        }

        //update cart
        public void AddToCart(int userId, int storeId, int itemId, int quantity)
        {
            // Use the cart manager to add items to the cart
            cartManager.AddToCart(userId, storeId, itemId, quantity);
        }
        public void RemoveFromCart(int userId, int storeId, int itemId, int quantity)
        {
            // Use the cart manager to remove items from the cart
            cartManager.RemoveFromCart(userId, storeId, itemId, quantity);
        }

        public void ClearCart(int userId)
        {
            // Use the cart manager to clear the cart and restock the store
            cartManager.Restock(userId);
        }
        public void DisplayCart(int userId)
        {
            // Use the cart manager to display the cart
            cartManager.DisplayCart(userId);
        }
        public void Checkout(int userId, string paymentMethod)
        {
            // Use the cart manager to get the total price
            decimal totalPrice = cartManager.GetTotalPrice(userId);
            Console.WriteLine($"Total Price: {totalPrice}");

            // Use the payment manager to process the payment
            paymentManager.MakePayment(userId, totalPrice, paymentMethod);
        }



    }
}