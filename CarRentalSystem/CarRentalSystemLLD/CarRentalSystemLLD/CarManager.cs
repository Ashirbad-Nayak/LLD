using CarRentalSystemLLD.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystemLLD
{
    public class CarManager
    {
        private static readonly Object _lock = new object();
        private static CarManager _instance;
        public Dictionary<int, Car> Cars { get; set; } = new Dictionary<int, Car>();
        private BookingManager bookingManager { get; set; }
        private CarManager() 
        {
            //private constructor to prevent instantiation
        }
        public static CarManager GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new CarManager();
                }
            }
            return _instance;
        }
        public void AddCar(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(nameof(car), "Car cannot be null");
            }
            if (Cars.ContainsKey(car.CarId))
            {
                throw new ArgumentException($"Car with ID {car.CarId} already exists.");
            }
            Cars[car.CarId] = car;
        }
        public void RemoveCar(int carId)
        {
            if (!Cars.ContainsKey(carId))
            {
                throw new KeyNotFoundException($"Car with ID {carId} not found.");
            }
            Cars.Remove(carId);
        }
        public Car GetCar(int carId)
        {
            if (!Cars.ContainsKey(carId))
            {
                throw new KeyNotFoundException($"Car with ID {carId} not found.");
            }
            return Cars[carId];
        }
        public List<Car> GetAllCars(DateTime startDate, DateTime endDate)
        {

            bookingManager = BookingManager.GetInstance();
            //validate startdate < = enddate
            if (endDate < startDate)
            {
                Console.WriteLine("End date cannot be earlier than start date.");
                return new List<Car>();
            }
            //check in booking manager waht all cars are availble within the dates mentioned by user
            List<Booking> bookings = bookingManager.GetAllBookings();

            //get all the car ids which are  available within the dates mentioned by user
            //important condition
            List<int> bookedCarIds = bookings.Where(b => b.BookingStatusType == Domain.Enum.BookingStatusTypeEnum.PENDING && (b.StartDate <= endDate && b.EndDate >= startDate)).Select(b => b.CarId).ToList();
            List<int> availableCarIds = Cars.Keys.Except(bookedCarIds).ToList();

            DisplayAvailableCars(availableCarIds);
            return Cars.Values.ToList();
        }
        public void UpdateCarAvailability(int carId, bool isAvailable)
        {
            if (!Cars.ContainsKey(carId))
            {
                throw new KeyNotFoundException($"Car with ID {carId} not found.");
            }
            Cars[carId].SetCarAvailability(isAvailable);
        }
        public void DisplayAvailableCars(List<int> availableCarIds)
        {
            Console.WriteLine("Available Cars:");
            foreach (var carId in availableCarIds)
            {
                var car  = Cars[carId];
                //show only few details of car
                Console.WriteLine($"Car ID: {car.CarId}, Name: {car.CarName}, Model: {car.CarModel}, PricePerDay: {car.PricePerDay}");
            }
        }
        public void DisplayCarDetails(int carId)
        {
            if (!Cars.ContainsKey(carId))
            {
                throw new KeyNotFoundException($"Car with ID {carId} not found.");
            }
            Cars[carId].DisplayCarDetails();
        }

        //calculate price based on  startdate and end date
        public double CalculatePrice(int carId, string startDate, string endDate)
        {
            if (!Cars.ContainsKey(carId))
            {
                throw new KeyNotFoundException($"Car with ID {carId} not found.");
            }
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);

            if (end < start)
            {
                throw new ArgumentException("End date cannot be earlier than start date.");
            }

            int numberOfDays = (end - start).Days;

            return Cars[carId].PricePerDay * numberOfDays;
        }




    }
}
