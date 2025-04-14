
namespace ATMLLD{
    public class Card {
        private int Id { get; set; }
        private string CardNumber { get; set; }
        private string ExpiryDate { get; set; }
        private string CardHolderName { get; set; } 
        private string CVV { get; set; }
        private string Pin { get; set; } = "0000"; // Default PIN
        private bool IsBlocked { get; set; } = false; // Default state
        private BankAccount BankAccount { get; set; } // Reference to the associated bank account

        public Card(int id, string cardNumber, string expiryDate, string cardHolderName, string cvv, BankAccount bankAccount)
        {
            Id = id;
            CardNumber = cardNumber;
            ExpiryDate = expiryDate;
            CardHolderName = cardHolderName;
            CVV = cvv;
            BankAccount = bankAccount;
        }

        public bool ValidatePin(string inputPin)
        {
            return inputPin == Pin? true : false;
        }

        public bool Withdraw(double amount)
        {
            if (!IsBlocked)
            {
                if (BankAccount.GetBalance() >= amount)
                {
                    BankAccount.Withdraw(amount);
                    return true;
                }
                else
                {
                    Console.WriteLine("Insufficient balance.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Card is blocked. Cannot withdraw.");
                return false;
            }
        }

        public double GetBalance()
        {
            if (!IsBlocked)
            {
                return BankAccount.GetBalance();
            }
            else
            {
                Console.WriteLine("Card is blocked. Cannot check balance.");
                return 0;
            }
        }
        public void BlockCard()
        {
            IsBlocked = true;
            Console.WriteLine("Card has been blocked.");
        }
        public void UnblockCard()
        {
            IsBlocked = false;
            Console.WriteLine("Card has been unblocked.");
        }

        

    }
}