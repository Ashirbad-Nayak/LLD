
using ATMLLD.WithdrawProcessorContainer;
using ATMLLD;
namespace ATMLLD.States
{
    public class WithdrawState : ATMState
    {
        public WithdrawState(ATM atm, Card card) : base(atm, card)
        {
            Console.WriteLine("Enter the amount to withdraw:");
        }
        public  override void Withdraw(string input)
        {
            if (int.TryParse(input, out int amount))
            {
                //check if atm has enough cash
                if(atm.GetBalance()>= amount)
                {
                    //check if card has enough balance
                    if (card.GetBalance() >= amount)
                    {
                        //withdraw cash
                        WithdrawProcessor withdrawProcessor = new TwoThousandWithdrawProcessor(new FiveHundredWithdrawProcessor(new OneHundredWithdrawProcessor(null)));
                        withdrawProcessor.ProcessWithdrawal(atm, amount);
                        //update card balance
                        card.Withdraw(amount);
                        Console.WriteLine($"Withdrawal of {amount} successful.");
                        Exit();
                    }
                    else
                    {
                        Console.WriteLine("Insufficient balance in your account.");
                        Exit();
                    }
                }
                else
                {
                    Console.WriteLine("ATM does not have enough cash.");
                    Console.WriteLine("Please try again later.");
                    Exit();
                }
            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a valid number.");
            }
        }

    }
}