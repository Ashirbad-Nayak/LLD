using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingLLD.Payment
{
    public interface IPaymentStrategy
    {
        bool ProcessPayment(int bookingId, decimal amount);
    }
}
