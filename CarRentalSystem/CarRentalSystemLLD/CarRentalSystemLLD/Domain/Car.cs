using CarRentalSystemLLD.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystemLLD.Domain
{
    public class Car
    {
        private static int _carIdCounter = 0;
        public int CarId { get; private set; }
        public string CarName { get; private set; }
        public CarTypeEnum CarType { get; private set; }
        public string CarNumber { get; private set; }
        public string CarModel { get; private set; }
        public int NumberOfSeats { get; private set; } = 4; // Default to 4 seats, can be set later
        public EngineTypeEnum EngineType { get; private set; }
        public string ImageUrl { get; private set; }
        public bool IsAvailable { get; private set; }
        public double PricePerDay { get; private set; }
        public int DefaultKM { get; private set; } = 100; // Default to 100 km, can be set later
        public double ExtraChargesPerHourAfterDefaultKM { get; private set; } = 25.0; // Default to 0.0, can be set later .Ideally is an hourly price after certain km
        
        public double defaultPrice { get; private set; } = 500.0; // Default to 0.0, can be set later
        public List<string> features { get; set; } = new List<string>(); // Default to empty list, can be set later
        public Car(int carId, string carName, CarTypeEnum carType, string carNumber, string carModel, int numberOfSeats, EngineTypeEnum engineType, string imageUrl, double pricePerDay, List<string> features)
        {
            CarId = _carIdCounter++;
            CarName = carName;
            CarType = carType;
            CarNumber = carNumber;
            CarModel = carModel;
            NumberOfSeats = numberOfSeats;
            EngineType = engineType;
            ImageUrl = imageUrl;
            IsAvailable = true;
            PricePerDay = pricePerDay;
            this.features = features;
        }
        public void SetCarAvailability(bool isAvailable)
        {
            IsAvailable = isAvailable;
        }
        public void DisplayCarDetails()
        {
            //display all information of car not only feature bu talso other detials
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"Car ID: {CarId}, Name: {CarName}, Model: {CarModel}");
            Console.WriteLine($">>> CarType: {CarType.ToString()}, NumberOfSeats: {NumberOfSeats}, EngineType: {EngineType}, Image: {ImageUrl}");
            Console.WriteLine($">>> DefaultKM: {DefaultKM}, ExtraChargesPerHourAfterDefaultKM: {ExtraChargesPerHourAfterDefaultKM}");
            Console.WriteLine($">>> DefaultPrice: {defaultPrice} , PricePerDay: {PricePerDay}");
            Console.WriteLine($">>> IsAvailable: {IsAvailable}");
            Console.WriteLine($">>> Features: {string.Join(',',features)}");
        }

    }
}
