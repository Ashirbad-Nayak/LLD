

namespace VendingMachineDesign.VendingMachineLLD.States
{
    public class DispenseState : VendingMachineState
    {
        private readonly VendingMachine vendingMachine;

        public DispenseState(VendingMachine vendingMachine, Item item, int quantity) : base(vendingMachine, item, quantity)
        {
            this.vendingMachine = vendingMachine;
        }

        public override void DispenseProduct()
        {
            if (item != null && quantity > 0)
            {
                Console.WriteLine($"Dispensing {quantity} of item {item.Name}.");
                vendingMachine.ReduceItemQuantity(item.Id, quantity);
                Console.WriteLine("Please collect your item.");
                Exit();
            }
            else
            {
                Console.WriteLine("No item to dispense.");
            }
        }
    }
}