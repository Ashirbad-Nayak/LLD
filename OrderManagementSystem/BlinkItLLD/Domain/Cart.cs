
namespace BlinkItLLD.Domain{
    public class Cart{
        public int UserId { get; set; }
        public int StoreId { get; set; }//needed to  restock the store if payment fails
        public Dictionary<int,CartItem> items { get; set; } // map of itemId to cartItem

        public Cart(int userId, int storeId)
        {
            UserId = userId;
            StoreId = storeId;
            items = new Dictionary<int,CartItem>();
        }

        public void AddItem(Item item, int quantity)
        {
            if (!items.ContainsKey(item.Id))
            {
                //create cartitem
                CartItem cartItem = new CartItem
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = quantity
                };
                items.Add(item.Id, cartItem);
                items[item.Id].Quantity = quantity;
            }
            else
            {
                items[item.Id].Quantity += quantity;
            }
            Console.WriteLine($"Added {quantity} of {item.Name} to the cart.");
        }
        public void RemoveItem(int itemId, int quantity)
        {
            if (items.ContainsKey(itemId))
            {
                items[itemId].Quantity -= quantity;
                if (items[itemId].Quantity <= 0)
                {

                    Console.WriteLine($"Removed {items[itemId].Name} from the cart.");
                    items.Remove(itemId);
                }
                else
                {
                    Console.WriteLine($"Removed {quantity} of {items[itemId].Name} from the cart.");
                }
            }

        }
        public void ClearCart()
        {
            items.Clear();
        }
        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (var item in items)
            {
                totalPrice += item.Value.Price * item.Value.Quantity;
            }
            return totalPrice;
        }
        public void DisplayCart()
        {
            Console.WriteLine(new String('=', 50)); //adding a separator line
            Console.WriteLine($"Cart for User ID: {UserId}");
            foreach (var item in items)
            {
                Console.WriteLine($"Item ID: {item.Value.Id}, Name: {item.Value.Name}, Price: {item.Value.Price * item.Value.Quantity}, Quantity: {item.Value.Quantity}");
            }
            Console.WriteLine($"Total Price: {GetTotalPrice()}");
        }
    }
}