using ParkingSystemLLD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD.PriceCalculatorStrategy
{
    public class DefaultPriceCalculatorStrategy : IPriceCalculatorStrategy
    {
        public double CalculatePrice(Reservation reservation)
        {
            return reservation.ParkingSpot.DefaultRate;
        }
    }
}
