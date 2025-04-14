
using ATMLLD;
namespace ATMLLD.WithdrawProcessorContainer
{
    public class TwoThousandWithdrawProcessor : WithdrawProcessor
    {
        public TwoThousandWithdrawProcessor(WithdrawProcessor withdrawProcessor) : base(withdrawProcessor)
        {
            // Constructor to set next processor
        }
        public override void ProcessWithdrawal(ATM atm, double amount)
        {
            //Check if amount>2000 , how many 2k notes will be used
            int twoKNotes = (int)(amount / 2000);
            int _2knotes = Math.Min(atm.Get2kNotes(),twoKNotes);

            //update atm
            atm.Update_2k_Notes(_2knotes);
            //update amount
            amount -= _2knotes * 2000;
            if(amount == 0)
            return;
            nextProcessor?.ProcessWithdrawal(atm, amount);
        }
    }
}