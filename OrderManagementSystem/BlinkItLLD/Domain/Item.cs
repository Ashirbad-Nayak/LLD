using BlinkItLLD.Domain.Enums;
using System;
namespace BlinkItLLD.Domain
{
    public class Item
    {
        private static int _idCounter = 0; // Static counter for unique ID generation
        public int Id { get; private set; } // Unique ID for the item
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Item(string name, decimal price, int quantity)
        {
            Id = ++_idCounter; // Increment and assign unique ID
            Name = name;
            Price = price;
            Quantity = quantity;
        }
        public void UpdateItem(OperationTypeEnum operationType,int quantity)
        {
            if (operationType == OperationTypeEnum.ADD)
            {
                Quantity += quantity;
            }
            else if (operationType == OperationTypeEnum.REMOVE)
            {
                Quantity -= Math.Min(quantity, Quantity);// Ensure quantity doesn't go negative
            }
            Console.WriteLine($"Item- {Name} updated. New quantity: {Quantity}");
        }
        public void DisplayItem()
        {
            Console.WriteLine($"Item ID: {Id}, Name: {Name}, Price: {Price}, Quantity: {Quantity}");
        }
    }
}