using System.Collections.Generic;
using BlinkItLLD.Domain;
using BlinkItLLD.Domain.Enums;
using System;
namespace BlinkItLLD.StoreFinderStrategy
{
    public interface IStoreFinderStrategy
    {
        // Define method signatures for the strategy
        Store FindStore(Location location, List<Store> stores, string itemName);

    }
}