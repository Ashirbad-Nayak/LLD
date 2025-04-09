using ParkingSystemLLD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD.ParkingSpotStrategy
{
    public interface IParkingSpotStrategy
    {
        public List<ParkingSpot> GetParkingSpot(List<ParkingSpot> parkingSpots);
    }
}
