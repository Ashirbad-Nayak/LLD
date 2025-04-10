using CarRentalSystemLLD.Domain;

namespace CarRentalSystemLLD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Car Rental System");
            //create cars and add to carmanager
            CarManager carManager = CarManager.GetInstance();
            List<Car> cars = CreateCars();
            foreach (Car car in cars)
            {
                carManager.AddCar(car);
            }
            //find avaialble cars for a location,start date and enddate

            string startDate = "2023-10-01";
            string endDate = "2023-10-05";
            Console.WriteLine($"Finding available cars from {startDate} to {endDate}");
            carManager.GetAllCars(DateTime.Parse(startDate), DateTime.Parse(endDate));

            //select car
            int carId = 1;
            carManager.DisplayCarDetails(carId);
            Console.WriteLine($"User selected the car {carId}");
            //calculate price
            double price = carManager.CalculatePrice(carId, startDate, endDate);
            //display price
            Console.WriteLine($"Price for car {carId} from {startDate} to {endDate} is {price}");
            //create a booking manager
            BookingManager bookingManager = BookingManager.GetInstance();

            //Create booking
            Booking booking = new Booking(carId, 1, DateTime.Parse(startDate), DateTime.Parse(endDate), "Location1", "Location2");
            bookingManager.CreateBooking(booking);

            //pay
            bookingManager.MakePayment(booking.BookingId, "CARD");
            carManager.GetAllCars(DateTime.Parse(startDate), DateTime.Parse(endDate));

            Task.Delay(7000).Wait();


        }

        public static List<Car> CreateCars()
        {
            //create a list off feature form feature class
            List<string> features = new List<string>(){
                Feature.AIR_CONDITIONING, Feature.BLUETOOTH, Feature.POWER_DOORS, Feature.REMOTE_START };
            List<Car> cars = new List<Car>();

            for (int i=0; i< 3; i++)
            {
                Car car = new Car(i, "Car-" + i, Domain.Enum.CarTypeEnum.SUV, "ABC-" + i, "Model-" + i, 4, Domain.Enum.EngineTypeEnum.PETROL, "https://ImageUrl/" + i, 500.0 + i * 100, features);
                
               cars.Add(car);
            }
            for (int i = 3; i < 6; i++)
            {
                Car car = new Car(i, "Car-" + i, Domain.Enum.CarTypeEnum.LUXURY, "ABC-" + i, "Model-" + i, 7, Domain.Enum.EngineTypeEnum.DIESEL, "https://ImageUrl/" + i, 500.0 + i * 500, features);
                cars.Add(car);

            }
            for (int i = 7; i < 10; i++)
            {
                Car car = new Car(i, "Car-" + i, Domain.Enum.CarTypeEnum.HATCHBACK, "ABC-" + i, "Model-" + i, 8, Domain.Enum.EngineTypeEnum.PETROL, "https://ImageUrl/" + i, 500.0 + i * 200, features);
                cars.Add(car);

            }
            return cars;
        }
    }
}
