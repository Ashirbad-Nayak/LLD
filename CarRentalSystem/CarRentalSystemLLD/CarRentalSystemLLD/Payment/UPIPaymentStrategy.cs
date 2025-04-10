using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD.Payment
{
    public class UPIPaymentStrategy : IPaymentStrategy
    {
        public bool ProcessPayment(int bookingId, double amount)
        {
            // Simulate UPI payment processing
            Console.WriteLine($"Processing UPI payment of {amount}...");

            //blackboxing the logic
           //consider it fails always
           //Doing it so that we can visualize how seats are released and made available whne payment fails
            return false;
        }
    }
}
