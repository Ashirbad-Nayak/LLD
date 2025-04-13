

namespace BlinkItLLD.Payment
{
    public class CashPaymentStrategy : IPaymentStrategy
    {
        public bool ProcessPayment(int cartId, decimal amount)
        {
            // Simulate UPI payment processing
            Console.WriteLine($"Recorded cash payment");

            // Simulate payment success
            return true;
        }
    }
}
