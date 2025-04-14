using ATMLLD;
namespace ATMLLD.States
{
    public class CheckBalanceState : ATMState
    {
        public CheckBalanceState(ATM atm, Card card) : base(atm, card)
        {
            Console.WriteLine("Checking balance...");
        }


        public override void CheckBalance()
        {
            Console.WriteLine($"Your balance is: {card.GetBalance()}");
            Exit();
        }
    }
}