using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkItLLD.Payment
{
    public class UPIPaymentStrategy : IPaymentStrategy
    {
        public bool ProcessPayment(int cartId, decimal amount)
        {
            // Simulate UPI payment processing
            Console.WriteLine($"Processing UPI payment of {amount}...");

            // Simulate payment failure
            return false;
        }
    }
}
