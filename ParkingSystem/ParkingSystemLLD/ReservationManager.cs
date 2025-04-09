using ParkingSystemLLD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD
{
   public class ReservationManager
    {
        private Dictionary<string, Reservation> reservations = new Dictionary<string, Reservation>();
        private static readonly Object _lock = new Object();
        private static ReservationManager reservationManagerInstance;
        private ReservationManager()
        {
            //private constructor
        }
        public static ReservationManager GetReservationManagerInstane()
        {
            if (reservationManagerInstance is null)
            {
                lock (_lock)
                {
                    if (reservationManagerInstance is null)
                    {
                        reservationManagerInstance = new ReservationManager();
                    }
                }
            }
            return reservationManagerInstance;
        }
        public Reservation GetReservation(string vehicleNumber)
        {
            if (reservations.ContainsKey(vehicleNumber))
            {
                return reservations[vehicleNumber];
            }
            else
            {
                Console.WriteLine("Reservation not found");
                return null;
                //throw new Exception("Reservation not found");
            }
        }
        public Reservation CreateReservation(Vehicle vehicleInfo, ParkingSpot parkingSpot)
        {
            Reservation reservation = new Reservation(vehicleInfo, parkingSpot);
            reservations.Add(vehicleInfo.VehicleNumber, reservation);
            return reservation;
        }
        public void UpdateReservation(string vehicleNumber)
        {
            if (reservations.ContainsKey(vehicleNumber))
            {
                reservations.Remove(vehicleNumber);
            }
            else
            {
                Console.WriteLine("Reservation not found");
                //throw new Exception("Reservation not found");
            }
        }
    }
}
