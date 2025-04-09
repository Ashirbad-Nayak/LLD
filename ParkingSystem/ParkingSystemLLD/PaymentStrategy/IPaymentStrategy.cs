﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD.PaymentStrategy
{
    public interface IPaymentStrategy
    {
        public void ProcessPayment(double amount);
    }
}
