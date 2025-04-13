
using BlinkItLLD.Domain.Enums;
namespace BlinkItLLD.Domain
{
    public class Store
    {
        private static int _idCounter = 0;
        public int Id { get;}
        public string Name { get; set; }
        public Location Location { get; set; }
        public Dictionary<int, Item> Items { get; set; } = new Dictionary<int, Item>();

        public Store(int id, string name, string address, Location location)
        {
            Id = _idCounter++;
            Name = name;
            Location = location;
        }
        public void AddItem(Item item)
        {
            if (Items.ContainsKey(item.Id))
            {
                Console.WriteLine("Item already exists in the store.");
                return;
            }
            Items.Add(item.Id, item);
        }
        public void RemoveItem(int itemId)
        {

            if (Items.ContainsKey(itemId))
            {
                Items.Remove(itemId);
            }
            else
            {
                Console.WriteLine("Item not found in the store.");
            }
        }
        public Item GetItem(int itemId)
        {
            if (Items.ContainsKey(itemId))
            {
                return Items[itemId];
            }
            return null;
        }
        public List<Item> GetAllItems()
        {
            return Items.Values.ToList();
        }
        public void UpdateItemQuantity(int itemId, int quantity, OperationTypeEnum operationType)
        {
            if (Items.ContainsKey(itemId))
            {
                Items[itemId].UpdateItem(operationType, quantity);
            }
            else
            {
                Console.WriteLine("Item not found in the store.");
            }
        }
        public void DisplayItems()
        {
            Console.WriteLine($"Items in Store {Name}:");
            foreach (var item in Items.Values)
            {
                item.DisplayItem();
            }
        }
        public void DisplayStoreInfo()
        {
            Console.WriteLine($"Store ID: {Id}, Name: {Name}, Location: ({Location.Latitude}, {Location.Longitude})");
        }

    }
}