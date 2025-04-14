using ATMLLD;
namespace ATMLLD.States
{
    public class SelectOperationState : ATMState
    {

        public SelectOperationState(ATM atm, Card card) : base(atm, card)
        {
            Console.WriteLine("Select an operation:");
            Console.WriteLine("1. Withdraw");
            Console.WriteLine("2. Check Balance");
            Console.WriteLine("3. Exit");
        }

        public override void HandleInput(string input)
        {
            switch (input)
            {
                case "Withdraw":
                    atm.SetState(new WithdrawState(atm, card));
                    break;
                case "CheckBalance":
                    atm.SetState(new CheckBalanceState(atm, card));
                    break;
                case "Exit":
                    Exit();
                    break;
                default:
                    Console.WriteLine("Invalid operation. Please select again.");
                    Console.WriteLine("1. Withdraw");
                    Console.WriteLine("2. Check Balance");
                    Console.WriteLine("3. Exit");
                    break;
            }
        }

    }
}