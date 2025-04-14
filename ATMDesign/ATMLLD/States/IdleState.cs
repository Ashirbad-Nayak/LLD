using ATMLLD;
namespace ATMLLD.States
{
    public class IdleState : ATMState
    {
        public IdleState(ATM atm) : base(atm, null)
        {
            Console.WriteLine(new string('-', 20));
        }
        public override void InsertCard(Card card)
        {
            Console.WriteLine("Card inserted.");
            atm.SetState(new HasCardState(atm, card));
        }

    }
}