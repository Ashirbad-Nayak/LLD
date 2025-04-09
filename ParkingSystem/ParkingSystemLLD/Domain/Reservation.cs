using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD.Domain
{
    public class Reservation
    {
        private static int _idCounter = 0;
        public int ReservationId { get; private set; }
        public Vehicle VehicleInfo { get; set; }
        public ParkingSpot ParkingSpot { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public double? Price { get; set; }

        //can not create reservation withput vehicle and parking info
        //hence constructor
        //if can create , and alter we can set properties, simple get set would ahve been enough
        public Reservation(Vehicle vehicleInfo, ParkingSpot parkingSpot)
        {
            ReservationId = ++_idCounter;
            VehicleInfo = vehicleInfo;
            ParkingSpot = parkingSpot;
            EntryTime = DateTime.Now;
        }

    }
}
