using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingLLD.Payment
{
    public class CreditCardPaymentStrategy : IPaymentStrategy
    {
        public bool ProcessPayment(int bookingId, decimal amount)
        {
            // Simulate credit card payment processing
            Console.WriteLine($"Processing credit card payment of {amount}...");

            // Assume payment is successful
            return true;
        }
    }
}
