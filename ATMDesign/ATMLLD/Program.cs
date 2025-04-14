
using ATMLLD.States;

namespace ATMLLD{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ATM");
            Console.WriteLine(new string('-', 20));

            //Set up ATM
            ATM atm = new ATM(10, 10, 10); // 10 notes of each denomination

            //set up bank account
            BankAccount bankAccount = new BankAccount("AccountNumber"+Guid.NewGuid(), "Test User", 5000, AccountType.Savings, "Test Branch"); // Account with 1000 balance
            //set up Card
            Card card = new Card(1,"1234-5678-9012-3456", "12/25","Test User", "123",bankAccount); // Card with 1000 balance

            //set up user
            User user = new User("Test User", 1, card); // User with card

            //display atm
            atm.Display();
            //display atm balance
            Console.WriteLine($"ATM Balance: {atm.GetBalance()}");

            //display user
            user.DisplayUserInfo();
             // Display card balance
            Console.WriteLine($"Card Balance: {card.GetBalance()}");

            Console.WriteLine(new string('-', 20));

            
            Console.WriteLine("Please insert your card.");
            atm.GetState().InsertCard(card); // Insert card
            atm.GetState().EnterPin("0000"); // Validate PIN
            atm.GetState().HandleInput("Withdraw"); // Select withdraw operation
            atm.GetState().Withdraw("4600"); // Withdraw amount
            
            Console.WriteLine(new string('-', 20));

            //display atm balance
            Console.WriteLine($"ATM Balance: {atm.GetBalance()}");
            
             // Display card balance
            Console.WriteLine($"Card Balance: {card.GetBalance()}");

            Console.WriteLine(new string('-', 20));

            //Start check balance process
            Console.WriteLine("Please insert your card.");
            atm.GetState().InsertCard(card); // Insert card
            atm.GetState().EnterPin("0000"); // Validate PIN
            atm.GetState().HandleInput("CheckBalance"); // Select CheckBalance operation
            atm.GetState().CheckBalance(); // Display balance

            Console.WriteLine(new string('-', 20));

            //display atm balance
            Console.WriteLine($"ATM Balance: {atm.GetBalance()}");
            
             // Display card balance
            Console.WriteLine($"Card Balance: {card.GetBalance()}");

            //Show number of notes in ATM
            Console.WriteLine("ATM Notes: 2K_NOTES: {0}, 500_NOTES: {1}, 100_NOTES: {2}",
             atm.Get2kNotes(), atm.Get500Notes(), atm.Get100Notes());

        

        }
    }
}