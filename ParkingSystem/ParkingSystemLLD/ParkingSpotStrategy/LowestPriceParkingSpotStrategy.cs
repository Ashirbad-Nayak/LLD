using ParkingSystemLLD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD.ParkingSpotStrategy
{
    public class LowestPriceParkingSpotStrategy : IParkingSpotStrategy
    {
        public List<ParkingSpot> GetParkingSpot(List<ParkingSpot> parkingSpots)
        {
            List<ParkingSpot> availableParkingSpots = parkingSpots.Where(parkingSpot => parkingSpot.IsAvailable).ToList();
            if (availableParkingSpots.Count == 0)
            {
                Console.WriteLine("No parking spots available");
                //throw new Exception("No parking spots available");
            }
            return availableParkingSpots.OrderBy(parkingSpot => parkingSpot.MinuteRate).ToList();//blackboxing the strategy
        }
    }
}
