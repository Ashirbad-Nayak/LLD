
namespace ATMLLD{
    public class BankAccount{
        public  string accountNumber { get; private set; }
        public string accountHolderName { get; private set; }
        private double balance;

        private AccountType accountType; // Enum for account type (e.g., Savings, Checking)
        //branch info
        private string branchName;

        public BankAccount(string accountNumber, string accountHolderName, double initialBalance, AccountType accountType, string branchName)
        {
            this.accountNumber = accountNumber;
            this.accountHolderName = accountHolderName;
            this.balance = initialBalance;
            this.accountType = accountType;
            this.branchName = branchName;
        }
        public double GetBalance()
        {
            return balance;
        }
        public void Deposit(double amount)
        {
            if (amount > 0)
            {
                balance += amount;
                Console.WriteLine($"Deposited: {amount}. New Balance: {balance}");
            }
            else
            {
                Console.WriteLine("Deposit amount must be positive.");
            }
        }
        public void Withdraw(double amount)
        {
            if (amount > 0 && amount <= balance)
            {
                balance -= amount;
                Console.WriteLine($"Withdrawn: {amount}. New Balance: {balance}");
            }
            else
            {
                Console.WriteLine("Invalid withdrawal amount.");
            }
        }
        public void DisplayAccountInfo()
        {
            Console.WriteLine($"Account Number: {accountNumber}, Account Holder: {accountHolderName}, Balance: {balance}, Account Type: {accountType}, Branch: {branchName}");
        }
    }
}