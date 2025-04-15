
using VendingMachineDesign.VendingMachineLLD.States;
namespace VendingMachineDesign.VendingMachineLLD
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Vending Machine!");
            Console.WriteLine(new string('=', 20));
            // Create a vending machine
            VendingMachine vendingMachine = new VendingMachine();

            // Add products to the vending machine
            vendingMachine.AddItem("Coke", 1.50, 10);
            vendingMachine.AddItem("Pepsi", 1.25, 5);
            vendingMachine.AddItem("Water", 1.00, 20);

            Console.WriteLine(new string('-', 20));

            // Display available products
            Console.WriteLine("Available Items:");
            vendingMachine.DisplayItems();

            Console.WriteLine(new string('-', 20));

            // Simulate a user selecting a product and making a payment
            string selectedProductCode = "1";
            double amountPaid = 2.00;

            Console.WriteLine($"Selected Product Code: {selectedProductCode}");
            Console.WriteLine($"Amount Paid: {amountPaid}");

            var selectedProduct = vendingMachine.GetItem(int.Parse(selectedProductCode));
            selectedProduct.DisplayItemInfo();

            Console.WriteLine(new string('-', 20));

            vendingMachine.GetState().ChooseItem(int.Parse(selectedProductCode), 1);
            vendingMachine.GetState().AcceptCoin(amountPaid);
            vendingMachine.GetState().DispenseProduct();

            // Display remaining items in the vending machine
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("Remaining Items:");
            vendingMachine.DisplayItems();
            Console.WriteLine(new string('-', 20));

            //try with insufficient coin
             selectedProductCode = "1";
             amountPaid = 0.50;

            Console.WriteLine($"Selected Product Code: {selectedProductCode}");
            Console.WriteLine($"Amount Paid: {amountPaid}");

             selectedProduct = vendingMachine.GetItem(int.Parse(selectedProductCode));
            selectedProduct.DisplayItemInfo();

            Console.WriteLine(new string('-', 20));

            vendingMachine.GetState().ChooseItem(int.Parse(selectedProductCode), 1);
            vendingMachine.GetState().AcceptCoin(amountPaid);

            // Display remaining items in the vending machine
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("Remaining Items:");
            vendingMachine.DisplayItems();
            Console.WriteLine(new string('-', 20));


        }
    }
}