
namespace ATMLLD.WithdrawProcessorContainer
{
    public class FiveHundredWithdrawProcessor : WithdrawProcessor
    {
        public FiveHundredWithdrawProcessor(WithdrawProcessor withdrawProcessor): base(withdrawProcessor )
        {
            // Constructor to set next processor
        }
        public override void ProcessWithdrawal(ATM atm, double amount)
        {
            //Check if amount>500 , how many 500 notes will be used
            int fiveHundredNotes = (int)(amount / 500);
            int _500notes = Math.Min(atm.Get500Notes(), fiveHundredNotes);

            //update atm
            atm.Update_500_Notes(_500notes);
            //update amount
            amount -= _500notes * 500;
            if(amount == 0)
            return;
            nextProcessor?.ProcessWithdrawal(atm, amount);

        }
    }
}