
namespace ATMLLD.WithdrawProcessorContainer{
    public abstract class WithdrawProcessor
    {
        protected WithdrawProcessor nextProcessor;

        public  WithdrawProcessor(WithdrawProcessor nextProcessor)
        {
            this.nextProcessor = nextProcessor;
        }

        public abstract void ProcessWithdrawal(ATM atm, double amount);
    }
}