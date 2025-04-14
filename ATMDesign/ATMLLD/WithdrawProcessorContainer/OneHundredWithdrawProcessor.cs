
namespace ATMLLD.WithdrawProcessorContainer
{
    public class OneHundredWithdrawProcessor : WithdrawProcessor
    {
        public OneHundredWithdrawProcessor(WithdrawProcessor withdrawProcessor) : base(withdrawProcessor)
        {
            // Constructor to set next processor
        }

        public override void ProcessWithdrawal(ATM atm, double amount)
        {
            //Check if amount>100 , how many 100 notes will be used
            int oneHundredNotes = (int)(amount / 100);
            int _100notes = Math.Min(atm.Get100Notes(), oneHundredNotes);

            //update atm
            atm.Update_100_Notes(_100notes);
            //update amount
            amount -= _100notes * 100;
            if(amount == 0)
            return;
            nextProcessor?.ProcessWithdrawal(atm, amount);
        }
    }
}