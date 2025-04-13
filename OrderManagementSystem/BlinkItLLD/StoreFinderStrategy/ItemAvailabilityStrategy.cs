
using System;
using System.Collections.Generic;
using BlinkItLLD.Domain;
using BlinkItLLD.Domain.Enums;
namespace BlinkItLLD.StoreFinderStrategy
{

    public class ItemAvailabilityStrategy : IStoreFinderStrategy
    {
        public Store FindStore(Location location, List<Store> stores, string itemName)
        {
            // Implement the logic to find stores based on item availability
            foreach (var store in stores)
            {
                //return the first store that has the item
                if (store.GetAllItems().Exists(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine($"Store found: {store.Name} ");
                    return store;
                }
            }
            Console.WriteLine("No store found with the specified item.");
            return null;
        }
    }
}