using ParkingSystemLLD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD.PriceCalculatorStrategy
{
    public class MinutePriceCalulatorStrategy : IPriceCalculatorStrategy
    {
        public double CalculatePrice(Reservation reservation)
        {
            reservation.ExitTime = DateTime.Now;
            TimeSpan? duration = reservation.ExitTime - reservation.EntryTime;
            double totalMinutes = Math.Ceiling(duration.Value.TotalMinutes);//nullable so .value
            return totalMinutes * reservation.ParkingSpot.MinuteRate;
        }
    }
}
