

namespace BlinkItLLD.Payment
{
    public class CreditCardPaymentStrategy : IPaymentStrategy
    {
        public bool ProcessPayment(int cartId, decimal amount)
        {
            // Simulate credit card payment processing
            Console.WriteLine($"Processing credit card payment of {amount}...");

            // Assume payment is successful
            return true;
        }
    }
}
