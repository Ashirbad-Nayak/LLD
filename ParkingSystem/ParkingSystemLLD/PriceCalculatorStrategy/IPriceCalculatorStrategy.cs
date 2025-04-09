using ParkingSystemLLD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD.PriceCalculatorStrategy
{
    public interface IPriceCalculatorStrategy
    {
        public double CalculatePrice(Reservation reservation);
    }
}
