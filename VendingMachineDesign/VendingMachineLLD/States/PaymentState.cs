
namespace VendingMachineDesign.VendingMachineLLD.States{
    public class PaymentState : VendingMachineState{

        public PaymentState(VendingMachine vendingMachine, Item item, int quantity) : base(vendingMachine, item, quantity){
            Console.WriteLine("Payment state: Please insert coins.");
        }
        public override void AcceptCoin(double amount){
            Console.WriteLine($"Accepted coin of amount: {amount}");

            
            //calculate total amount
            double totalAmount = item.Price * quantity;
            if (amount >= totalAmount){
                Console.WriteLine($"Total amount received: {amount}. Item price: {item.Price}. Quantity: {quantity}");
                if(amount > totalAmount){
                    Console.WriteLine($"Excess amount: {amount - totalAmount}. Please collect your change.");
                    Refund(amount - totalAmount);
                }

                vendingMachine.SetState(new DispenseState(vendingMachine, item, quantity));

            } else {
                Console.WriteLine($"Insufficient amount.");
                Refund(amount);
                Exit();
            }

        }
        public void Refund(double amount = 0){
            Console.WriteLine($"Refunding amount {amount}");
        }


    }
}