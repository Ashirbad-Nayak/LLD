
using BlinkItLLD.Domain;
using System.Collections.Generic;
using BlinkItLLD.Domain.Enums;
using System;
namespace BlinkItLLD
{
    public class StoreManager
    {
        
        private static readonly Object _lock = new Object();
        private static StoreManager _instance ;
        private Dictionary<int,Store> _stores = new Dictionary<int, Store>();
        private StoreManager()
        {
            // Initialize properties and collections here
        }
        public static StoreManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new StoreManager();
                    }
                    return _instance;
                }
            }
        }
        public void AddStore(Store store)
        {
            if (_stores.ContainsKey(store.Id))
            {
                Console.WriteLine("Store already exists.");
                return;
            }
            _stores.Add(store.Id, store);
        }
        public void RemoveStore(int storeId)
        {
            if (_stores.ContainsKey(storeId))
            {
                _stores.Remove(storeId);
            }
            else
            {
                Console.WriteLine("Store not found.");
            }
        }
        public Store GetStore(int storeId)
        {
            if (_stores.ContainsKey(storeId))
            {
                return _stores[storeId];
            }
            return null;
        }
        public List<Store> GetAllStores()
        {
            return _stores.Values.ToList();
        }
    }

}