using System;
using System.Collections.Generic;
using BlinkItLLD.Domain;
using BlinkItLLD.Domain.Enums;

namespace BlinkItLLD.OrderManagementSystem
{
    public class SearchManager
    {
        private static readonly Object _lock = new Object();
        private static SearchManager _instance;
        private StoreManager _storeManager;
        private SearchManager()
        {
            // Constructor logic here

            _storeManager = StoreManager.Instance; // Initialize StoreManager instance
        }

        public static SearchManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SearchManager();
                    }
                    return _instance;
                }
            }
        }

        public List<Item> SearchItems(int storeId, string itemName)
        {
            List<Item> result = new List<Item>();
            Store store = _storeManager.GetStore(storeId);
            if (store != null)
            {
                foreach (var item in store.GetAllItems())
                {
                    if (item.Name.Contains(itemName, StringComparison.OrdinalIgnoreCase))
                    {
                        result.Add(item);
                    }
                }
            }
            return result;
        }
        
    }
}