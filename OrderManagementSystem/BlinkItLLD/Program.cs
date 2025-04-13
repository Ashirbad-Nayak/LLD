// See https://aka.ms/new-console-template for more information
using BlinkItLLD.Domain.Enums;
using BlinkItLLD.Domain;
using System;
namespace BlinkItLLD{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, User - 1");
            Console.WriteLine("Welcome to BlinkIt Order Management System!");
            Console.WriteLine(new string('-', 50));


            //initialize order processor
            OrderProcessor orderProcessor = OrderProcessor.Instance;


            //set up stores and items
            StoreManager storeManager = orderProcessor.storeManager;
            Store store1 = new Store(1, "Store 1", "Address 1", new Location(12.9716, 77.5946));
            Store store2 = new Store(2, "Store 2", "Address 2", new Location(13.0358, 77.5970));
            storeManager.AddStore(store1);
            storeManager.AddStore(store2);

            Item item1 = new Item("Item 1", 10.0m, 100);
            Item item2 = new Item("Item 2", 20.0m, 50);
            Item item3 = new Item("Item 3", 30.0m, 200);
            Item item4 = new Item("Item 4", 40.0m, 150);
            Item item5 = new Item("Item 5", 50.0m, 80);
            store1.AddItem(item1);
            store1.AddItem(item2);
            store2.AddItem(item3);
            store2.AddItem(item4);
            store2.AddItem(item5);
            

            
            //set location and  find the nearest store
            int userId = 1; // User ID
            Location userLocation = new Location(12.9716, 77.5946); // User's location
            Store store = orderProcessor.FindAStore(userLocation);

            //log user info
            Console.WriteLine($"User ID: {userId}");
            Console.WriteLine($"User Location: {userLocation.Latitude}, {userLocation.Longitude}");
            Console.WriteLine($"Nearest Store: {store.Name}");
            //add separator line
            Console.WriteLine(new string('-', 50));

            //find all available items in the store
            Console.WriteLine($"Items available in the nearest store ({store.Name}):");
            store.DisplayItems();

            //add items to the cart
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Adding items to the cart...");
            int storeId = store.Id; // Store ID
            int itemId = item1.Id; // Item ID
            int quantity = 2; // Quantity to add
            orderProcessor.AddToCart(userId, storeId, itemId, quantity);

            //add different items to the cart
            orderProcessor.AddToCart(userId, storeId, item2.Id, 3);


            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Removing few items from the cart...");

            //remove few items from the cart
            orderProcessor.RemoveFromCart(userId, storeId, item1.Id, 1); // Remove 1 quantity of Item 1
            orderProcessor.RemoveFromCart(userId, storeId, item2.Id, 5); // Remove 5 quantity of Item 2

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Displaying cart items...");
            orderProcessor.DisplayCart(userId);
            Console.WriteLine(new string('-', 50));


            //checkout the cart and make payment
            Console.WriteLine("Checking out the cart...");
            orderProcessor.Checkout(userId, "CreditCard");
            Console.WriteLine("Thank you for your purchase!");

            //display store items
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Items available in the store after checkout:");
            store.DisplayItems();
            orderProcessor.DisplayCart(userId);



            Console.WriteLine(new string('-', 50));

            // place another order
            Console.WriteLine("Placing another order...");
            //add items to the cart
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Adding items to the cart...");
            orderProcessor.AddToCart(userId, storeId, itemId, 20);

            //add different items to the cart
            orderProcessor.AddToCart(userId, storeId, item2.Id, 40);


            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Removing few items from the cart...");

            //remove few items from the cart
            orderProcessor.RemoveFromCart(userId, storeId, item1.Id, 5); 
            orderProcessor.RemoveFromCart(userId, storeId, item2.Id, 20);

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Displaying cart items...");
            orderProcessor.DisplayCart(userId);
            Console.WriteLine(new string('-', 50));


            //checkout the cart and make payment
            Console.WriteLine("Checking out the cart...");
            orderProcessor.Checkout(userId, "UPI"); //failed payment

            //display store items
            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Items available in the store after checkout:");
            store.DisplayItems();
            orderProcessor.DisplayCart(userId);

            Console.WriteLine("Thank you for using BlinkIt Order Management System!");
        }
    }
}
