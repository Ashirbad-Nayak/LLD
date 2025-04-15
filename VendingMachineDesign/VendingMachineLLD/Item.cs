
namespace VendingMachineDesign.VendingMachineLLD
{
    public class Item
    {
        private static int _idCounter = 0;
        public int Id { get; private set; }
        public string Name { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }


        public Item(string name, double price, int quantity)
        {
            Id = ++_idCounter; // Auto-increment ID
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }
        public void ReduceQuantity(int quantity)
        {
            if (Quantity >= quantity)
            {
                Quantity -= quantity;
            }
            else
            {
                Console.WriteLine("Not enough quantity available.");
            }
        }
        public void DisplayItemInfo()
        {
            Console.WriteLine($"Item: {Name}, Price: {Price}, Quantity: {Quantity}");
        }
    }
}