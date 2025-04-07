using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD.Payment
{
    public interface IPaymentStrategy
    {
        bool ProcessPayment(int bookingId, double amount);
    }
}
