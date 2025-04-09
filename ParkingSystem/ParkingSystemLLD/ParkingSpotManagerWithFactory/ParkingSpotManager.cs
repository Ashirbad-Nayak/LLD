using ParkingSystemLLD.Domain;
using ParkingSystemLLD.ParkingSpotStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD.ParkingSpotManagerWithFactoryWithFactory
{
    public class ParkingSpotManager
    {
        private IParkingSpotStrategy _parkingSpotStrategy;
        private List<ParkingSpot> parkingSpots = new List<ParkingSpot>();



        public ParkingSpotManager(IParkingSpotStrategy parkingspotStrategy)
        {
            _parkingSpotStrategy = parkingspotStrategy;
        }
        public void AddParkingSpot(ParkingSpot parkingSpot)
        {
            parkingSpots.Add(parkingSpot);

        }
        public void RemoveParkingSpot(int parkingSpotId)
        {
            parkingSpots.RemoveAll(parkingSpot=> parkingSpot.Id != parkingSpotId);//remove by id 

        }
        public List<ParkingSpot>  GetParkingSpots()
        {
            if (parkingSpots.Count == 0)
            {
                Console.WriteLine("No parking spots available");
                //throw new Exception("No parking spots available");
            }
            List<ParkingSpot> avaialbleParkingSpots = _parkingSpotStrategy.GetParkingSpot(parkingSpots);
            if (!avaialbleParkingSpots.Any())
            {
                Console.WriteLine("No parking spots available");
                // throw new Exception("No parking spots available");
            }
            return avaialbleParkingSpots;
        }
        public void BookParkingSpot(int Id)
        {
            ParkingSpot parkingSpot = parkingSpots.FirstOrDefault(parkingSpot => parkingSpot.Id == Id);
            if (parkingSpot == null)
            {
                Console.WriteLine("Parking spot not found");
                // throw new Exception("Parking spot not found");
            }
            parkingSpot.Book();
        }
        public void ReleaseParkingSpot(int Id)
        {
            ParkingSpot parkingSpot = parkingSpots.FirstOrDefault(parkingSpot => parkingSpot.Id == Id);
            if (parkingSpot == null)
            {
               Console.WriteLine("Parking spot not found");
                //throw new Exception("Parking spot not found");
            }
            parkingSpot.Release();
        }

    }
}
