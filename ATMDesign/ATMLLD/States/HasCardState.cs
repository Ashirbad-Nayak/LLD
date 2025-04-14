using ATMLLD;
namespace ATMLLD.States{
    public class HasCardState : ATMState
    {
        public HasCardState(ATM atm, Card card) : base(atm, card)
        {
            Console.WriteLine("Card detected. Please enter your PIN.");
        }

        public override void EnterPin(string pin)
        {
            if (card.ValidatePin(pin))
            {
                Console.WriteLine("PIN validated.");
                atm.SetState(new SelectOperationState(atm, card));
            }
            else
            {
                Console.WriteLine("Invalid PIN.");
                Exit();
                atm.SetState(new IdleState(atm));
            }
        }

    }
}