
using System;
using System.Collections.Generic;
using BlinkItLLD.Domain;
using BlinkItLLD.Domain.Enums;


namespace BlinkItLLD.StoreFinderStrategy
{
    

    public class NearestStoreStrategy : IStoreFinderStrategy
    {
        public Store FindStore(Location location, List<Store> stores, string itemName)
        {
            Store nearestStore = null;
            double minDistance = double.MaxValue;

            foreach (var store in stores)
            {
                    double distance = CalculateDistance(location, store.Location);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestStore = store;
                    }
                
                
            }

            return nearestStore;
        }

        private double CalculateDistance(Location loc1, Location loc2)
        {
            // Implement a method to calculate the distance between two locations
            // For simplicity, using Euclidean distance here
            return Math.Sqrt(Math.Pow(loc1.Latitude - loc2.Latitude, 2) + Math.Pow(loc1.Longitude - loc2.Longitude, 2));
        }
    }

}