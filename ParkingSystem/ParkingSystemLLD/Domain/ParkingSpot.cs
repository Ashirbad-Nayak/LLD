using ParkingSystemLLD.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD.Domain
{
    public class ParkingSpot
    {
        public int Id { get; set; }
        public ParkingSpotTypeEnum ParkingSpotType { get; set; }
        public bool IsAvailable { get; set; } = true;
        public double HourlyRate { get; set; }
        public double DailyRate { get; set; }
        public double MinuteRate { get; set; }
        public double DefaultRate { get; set; } = 100;

        public void Book()
        {
            if (IsAvailable)
            {
                IsAvailable = false;
                Console.WriteLine("Parking spot booked successfully");
            }
            else
            {
                Console.WriteLine("Parking spot is already booked");
                //throw new Exception("Parking spot is already booked");
            }
        }
        public void Release()
        {
            if (!IsAvailable)
            {
                IsAvailable = true;
                Console.WriteLine("Parking spot released successfully");
            }
            else
            {
                Console.WriteLine("Parking spot is already available");
                //throw new Exception("Parking spot is already available");
            }
        }


    }
}
