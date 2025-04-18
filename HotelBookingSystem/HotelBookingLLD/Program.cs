
using HotelBookingLLD.Domain;
using HotelBookingLLD.Domain.HotelDomain;
using HotelBookingLLD.Domain.ClientDomain;
using HotelBookingLLD.Domain.ClientDomain.ClientDomainEnum;
using HotelBookingLLD.Domain.BookingDomain;
using HotelBookingLLD.Domain.BookingDomain.BookingEnum;
namespace HotelBookingLLD{
     public class Program
    {
        static void Main(string[] args)
        {
            HotelBookingProcessor hotelBookingProcessor = HotelBookingProcessor.GetInstance();

            //Create Hotels
            List<Hotel> hotelsToAdd = CreateHotels();
            foreach(Hotel hotel in hotelsToAdd){
                hotelBookingProcessor.AddHotel(hotel);
            }

            //first page
            //user selects dates and location
            DateTime checkInDate = DateTime.Now.AddDays(5);
            DateTime checkOutDate = DateTime.Now.AddDays(10);
            Random random = new Random();
            Location location = new Location
            (   
                random.NextDouble() * 180 - 90,
                random.NextDouble() * 360 - 180
            );

            //display user input
            Console.WriteLine($"User selected Check-In : {checkInDate.ToString()}, Check-Out: {checkOutDate.ToString()} with location: Lat {location.Latitude} & Long {location.Longitude} ");
            Console.WriteLine(new String('=',50));

            //Display hotels to user
           List<Hotel> filteredHotels = hotelBookingProcessor.FiterHotelsBasedOnLocationAndDate(checkInDate, checkOutDate, location);
            Console.WriteLine("Available Hotels: ");
            foreach(Hotel hotel in filteredHotels){
                Console.WriteLine();
                DisplayHotel(hotel);
            }
            Console.WriteLine(new String('=',50));

            //user filters and sorts
            HotelFilter hotelFilter = new HotelFilter(){
                MaxPrice = 2000.0m,
                MinRating = 3,
                RequiredAmenities = new List<string> {"WiFi", "Pool"}
            };

            Console.WriteLine($"Custom Filter: Hotel should have max price {hotelFilter.MaxPrice} with minimum rating of {hotelFilter.MinRating} with amenities like {string.Join(", ", hotelFilter.RequiredAmenities)}");

            UserSortPreference sortpref1 = new UserSortPreference(){
                Field = SortFieldEnum.Price,
                Order = SortOrderEnum.Ascending
            };
            UserSortPreference sortpref2 = new UserSortPreference(){
                Field = SortFieldEnum.Rating,
                Order = SortOrderEnum.Descending
            };

            List<UserSortPreference> userSortPrefs = new List<UserSortPreference>(){sortpref1, sortpref2};

            Console.WriteLine("User sort preference is hotels must be sorted by price (low to high) and by rating (high to low).");
            Console.WriteLine(new String('=',50));

           filteredHotels = hotelBookingProcessor.FilterHotelsByUserPreferences(filteredHotels, hotelFilter, userSortPrefs);
            Console.WriteLine("Available hotels after custom filter & sort : ");
            foreach(Hotel hotel in filteredHotels){
                Console.WriteLine();
                DisplayHotel(hotel);
            }
            Console.WriteLine(new String('=',50));

            //select 1 hotel and 1 room
            Hotel selectedHotel = filteredHotels[0];
            var rooms = selectedHotel.Rooms.Keys.ToList();
            List<int> selectedRoomIds = new List<int>(){rooms[0],rooms[1]};


            Console.WriteLine($"User Selected Hotel : {selectedHotel.Name} with rooms - {string.Join(", ",selectedRoomIds)}");
            Console.WriteLine(new String('=',50));

            //Create Booking
            int bookingId = hotelBookingProcessor.CreateBooking(checkInDate, checkOutDate, selectedHotel.Id, selectedRoomIds);

            //pay
            hotelBookingProcessor.MakePayment(bookingId, "CARD");  

            Console.WriteLine(new String('=',50));
            Console.WriteLine("Press any key to see available hotels");
            Console.ReadLine();
            
            filteredHotels = hotelBookingProcessor.FiterHotelsBasedOnLocationAndDate(checkInDate, checkOutDate, location);
            Console.WriteLine("Available Hotels: ");
            foreach(Hotel hotel in filteredHotels){
                Console.WriteLine();
                DisplayHotel(hotel);
            }



        }


        public static List<Hotel> CreateHotels(){
            Console.WriteLine("Setting Up Hotels");
            Console.WriteLine(new String('=',50));

            Random random = new Random();
            List<Hotel> hotels = new List<Hotel>();

            List<string> allAmenities = new List<string>
            {
                "WiFi", "Parking", "Pool", "Gym", "Breakfast", "AC", "Laundry", "Spa"
            };

            for (int star = 1; star <= 5; star++)
            {
                for (int i = 1; i <= 2; i++) // 2 hotels per star rating
                {
                    string hotelName = $"{star}-Star Hotel {i}";

                    // Random rating between 2.0 - 5.0
                    double hotelRating = Math.Round(Math.Clamp(random.NextDouble() * (star - 1) + star - 1, 1.0, 5.0), 1); // ~e.g. 3.2, 4.5

                    // Random location
                    Location location = new Location
                    (   
                        random.NextDouble() * 180 - 90,
                        random.NextDouble() * 360 - 180
                    );

                    // Random amenities
                    var randomAmenities = allAmenities.ToHashSet();
                        // .OrderBy(x => random.Next())
                        // .Take(random.Next(4, 6)) // 4 to 5 amenities
                        // .ToHashSet();


                    Hotel hotel = new Hotel(hotelName, (HotelTypeEnum)star, (float)hotelRating, randomAmenities, location);

                    // Add sample rooms
                    hotel.AddRoom(new Room(200.0m + star * 10, 1, RoomTypeEnum.Single));
                    hotel.AddRoom(new Room(300.0m + star * 20, 2, RoomTypeEnum.Double));
                    hotel.AddRoom(new Room(350.0m + star * 25, 2, RoomTypeEnum.TwinSharing));
                    DisplayHotel(hotel);
                    hotels.Add(hotel);
                }
            Console.WriteLine(new String('=',50));
            }
            return hotels;

        }

        public static void DisplayHotel(Hotel hotel){
            Console.WriteLine();
            
            Console.WriteLine($"Hotel Name: {hotel.Name} , {hotel.hotelTypeEnum.ToString()} , rating {hotel.Rating} , Location: Lat {hotel.Location.Latitude} & Long {hotel.Location.Longitude}");
            Console.WriteLine("Amenities: " + string.Join(", ", hotel.Amenities));
            Console.WriteLine("Price Per Night: " + hotel.PricePerNight);

            Console.WriteLine();


        }

    }
}