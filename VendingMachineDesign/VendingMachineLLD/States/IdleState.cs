
namespace VendingMachineDesign.VendingMachineLLD.States{
    public class IdleState : VendingMachineState
    {
        public IdleState(VendingMachine vendingMachine) : base(vendingMachine)
        {
            Console.WriteLine("Please select an item.");
        }

        public override void ChooseItem(int itemId, int quantity)
        {
            if (vendingMachine.IsItemAvailable(itemId, quantity))
            {
                Console.WriteLine($"Item {itemId} selected with quantity {quantity}.");
                var item = vendingMachine.GetItem(itemId);
                vendingMachine.SetState(new PaymentState(vendingMachine, item, quantity));
            }
            else
            {
                Console.WriteLine($"Item {itemId} is not available in the requested quantity.");
            }
        }
    }
}