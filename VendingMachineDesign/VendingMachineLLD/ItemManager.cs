

namespace VendingMachineDesign.VendingMachineLLD
{
    public class ItemManager
    {
        private static readonly Object _lock = new Object();
        private static ItemManager _instance;
        public Dictionary<int, Item> Items { get; private set; } = new Dictionary<int, Item>();
        private ItemManager()
        {
            // Private constructor to prevent instantiation
        }
        public static ItemManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ItemManager();
                    }
                    return _instance;
                }
            }
        }
        public void AddItem(string name, double price, int quantity)
        {
            Item newItem = new Item(name, price, quantity);
            Items.Add(newItem.Id, newItem);
        }
        public void RemoveItem(int itemId)
        {
            if (Items.ContainsKey(itemId))
            {
                Items.Remove(itemId);
            }
            else
            {
                Console.WriteLine("Item not found.");
            }
        }

        public void DisplayItems()
        {
            foreach (var item in Items.Values)
            {
                item.DisplayItemInfo();
            }
        }
        public Item GetItem(int itemId)
        {
            if (Items.ContainsKey(itemId))
            {
                return Items[itemId];
            }
            else
            {
                Console.WriteLine("Item not found.");
                return null;
            }
        }
        public List<Item> GetAllItems()
        {
            return Items.Values.ToList();
        }

        public bool IsItemAvailable(int itemId, int quantity)
        {
            if (Items.ContainsKey(itemId))
            {
                return Items[itemId].Quantity >= quantity;
            }
            return false;
        }

    }
}