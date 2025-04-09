using ParkingSystemLLD.Domain;
using ParkingSystemLLD.Domain.Enums;
using ParkingSystemLLD.ParkingSpotManagerWithFactory;
using ParkingSystemLLD.ParkingSpotManagerWithFactoryWithFactory;

namespace ParkingSystemLLD
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //set up parking system

            //create list of parking spots
            List<ParkingSpot> parkingSpots = CreateParkingSpots();

            //create parking spot manager
            ParkingSpotManager _2WheelerparkingSpotManager = ParkingSpotManagerFactory.GetParkingSpotManager(ParkingSpotTypeEnum.TwoWheeler);
            ParkingSpotManager _4WheelerparkingSpotManager = ParkingSpotManagerFactory.GetParkingSpotManager(ParkingSpotTypeEnum.FourWheeler);
            foreach (var parkingSpot in parkingSpots)
            {
                if (parkingSpot.ParkingSpotType == ParkingSpotTypeEnum.TwoWheeler)
                {
                    _2WheelerparkingSpotManager.AddParkingSpot(parkingSpot);
                }
                else
                {
                    _4WheelerparkingSpotManager.AddParkingSpot(parkingSpot);
                }
            }

            //create list of 2wheeler vehicle
            List<Vehicle> vehicles = Create2WheelerVehicles();
            //Create list of 4 wheeler vehicle
            List<Vehicle> vehicles4Wheeler = Create4WheelerVehicles();


            //entrygate
            EntryGate _2WheelerEntryGate = new EntryGate(ParkingSpotTypeEnum.TwoWheeler);
            List<ParkingSpot> availableParkingSpots = _2WheelerEntryGate.GetAvailableParkingSpots();
            int availableParkingSpotCount = availableParkingSpots.Count;
            foreach (var vehicle in vehicles)
            {
                if(availableParkingSpotCount <= 0)
                {
                    Console.WriteLine("No parking spots available");
                    break;
                }
                _2WheelerEntryGate.BookParkingSpot(availableParkingSpots[availableParkingSpotCount-1].Id);
                Console.WriteLine( vehicle.VehicleNumber +" is assigned parking spot - "+ availableParkingSpots[availableParkingSpotCount - 1].Id);

                Reservation reservation = _2WheelerEntryGate.CreateReservation(vehicle, availableParkingSpots[availableParkingSpotCount - 1]);
                Console.WriteLine($"Reservation - {reservation.ReservationId} created for vehicle: " + vehicle.VehicleNumber);
                availableParkingSpotCount--;
            }


            //exitgate
            ExitGate _2WheelerExitGate = new ExitGate(ParkingSpotTypeEnum.TwoWheeler);
            foreach (var vehicle in vehicles)
            {
                Reservation reservation = _2WheelerExitGate.GetReservation(vehicle);
                if (reservation == null)
                {
                    Console.WriteLine("No reservation found for vehicle: " + vehicle.VehicleNumber);
                    continue;
                }
                double price = _2WheelerExitGate.CalculatePrice(reservation);
                Console.WriteLine("Price for vehicle " + vehicle.VehicleNumber + ": " + price);
                _2WheelerExitGate.MakePayment(reservation, price);

                _2WheelerExitGate.ReleaseParkingSpot(reservation.ParkingSpot.Id);
                Console.WriteLine("Parking spot " + reservation.ParkingSpot.Id + " released for vehicle: " + vehicle.VehicleNumber);

                _2WheelerExitGate.UpdateReservation(vehicle.VehicleNumber);

            }

            //entrygate
            EntryGate _4WheelerEntryGate = new EntryGate(ParkingSpotTypeEnum.FourWheeler);
            availableParkingSpots = _4WheelerEntryGate.GetAvailableParkingSpots();
            availableParkingSpotCount = availableParkingSpots.Count;
            foreach (var vehicle in vehicles4Wheeler)
            {
                if (availableParkingSpotCount <= 0)
                {
                    Console.WriteLine("No parking spots available");
                    break;
                }
                _4WheelerEntryGate.BookParkingSpot(availableParkingSpots[availableParkingSpotCount - 1].Id);
                Console.WriteLine(vehicle.VehicleNumber + " is assigned parking spot - " + availableParkingSpots[availableParkingSpotCount - 1].Id);

                Reservation reservation = _4WheelerEntryGate.CreateReservation(vehicle, availableParkingSpots[availableParkingSpotCount - 1]);
                Console.WriteLine($"Reservation - {reservation.ReservationId} created for vehicle: " + vehicle.VehicleNumber);
                availableParkingSpotCount--;
            }
            //exitgate
            ExitGate _4WheelerExitGate = new ExitGate(ParkingSpotTypeEnum.FourWheeler);
            foreach (var vehicle in vehicles4Wheeler)
            {
                Reservation reservation = _4WheelerExitGate.GetReservation(vehicle);
                if (reservation == null)
                {
                    Console.WriteLine("No reservation found for vehicle: " + vehicle.VehicleNumber);
                    continue;
                }
                double price = _4WheelerExitGate.CalculatePrice(reservation);
                Console.WriteLine("Price for vehicle " + vehicle.VehicleNumber + ": " + price);
                _4WheelerExitGate.MakePayment(reservation, price);

                _4WheelerExitGate.ReleaseParkingSpot(reservation.ParkingSpot.Id);
                Console.WriteLine("Parking spot " + reservation.ParkingSpot.Id + " released for vehicle: " + vehicle.VehicleNumber);

                _4WheelerExitGate.UpdateReservation(vehicle.VehicleNumber);
            }
        }


        public static List<Vehicle> Create2WheelerVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles.Add(new Vehicle { VehicleNumber = "KA-01-HH-1234", UserInfo = new User() { Name = "Test1" } });
            vehicles.Add(new Vehicle { VehicleNumber = "KA-01-HH-1235", UserInfo = new User() { Name = "Test2" } });
            vehicles.Add(new Vehicle { VehicleNumber = "KA-01-HH-1236", UserInfo = new User() { Name = "Test3" } });
            vehicles.Add(new Vehicle { VehicleNumber = "KA-01-HH-1237", UserInfo = new User() { Name = "Test4" } });
            return vehicles;
        }

        public static List<Vehicle> Create4WheelerVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles.Add(new Vehicle { VehicleNumber = "KA-01-HH-1238", UserInfo = new User() { Name = "Test5" } });
            vehicles.Add(new Vehicle { VehicleNumber = "KA-01-HH-1239", UserInfo = new User() { Name = "Test6" } });
            vehicles.Add(new Vehicle { VehicleNumber = "KA-01-HH-1240", UserInfo = new User() { Name = "Test7" } });
            vehicles.Add(new Vehicle { VehicleNumber = "KA-01-HH-1241", UserInfo = new User() { Name = "Test8" } });
            return vehicles;
        }

        public static List<ParkingSpot> CreateParkingSpots()
        {
            List<ParkingSpot> parkingSpots = new List<ParkingSpot>();
            parkingSpots.Add(new ParkingSpot { Id = 1, ParkingSpotType = ParkingSpotTypeEnum.TwoWheeler, MinuteRate = 10, DailyRate = 80, HourlyRate = 20});
            parkingSpots.Add(new ParkingSpot { Id = 2, ParkingSpotType = ParkingSpotTypeEnum.TwoWheeler, MinuteRate = 10, DailyRate = 80, HourlyRate = 20 });
            parkingSpots.Add(new ParkingSpot { Id = 3, ParkingSpotType = ParkingSpotTypeEnum.FourWheeler, MinuteRate = 20, DailyRate = 160, HourlyRate = 40 });
            parkingSpots.Add(new ParkingSpot { Id = 4, ParkingSpotType = ParkingSpotTypeEnum.FourWheeler, MinuteRate = 20, DailyRate = 160, HourlyRate = 40 });
            parkingSpots.Add(new ParkingSpot { Id = 5, ParkingSpotType = ParkingSpotTypeEnum.FourWheeler, MinuteRate = 20, DailyRate = 160, HourlyRate = 40 });
            return parkingSpots;
        }

        
    }
}
