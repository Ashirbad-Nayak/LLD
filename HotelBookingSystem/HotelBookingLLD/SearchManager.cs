
using HotelBookingLLD.Domain;
using HotelBookingLLD.Domain.HotelDomain;
using HotelBookingLLD.Domain.ClientDomain;
using HotelBookingLLD.Domain.ClientDomain.ClientDomainEnum;
// using HotelBookingLLD.Domain.BookingDomain;
using HotelBookingLLD.LocationStrategy;

namespace HotelBookingLLD{
    public class SearchManager{
        private static readonly Object _lock = new Object();
        private static SearchManager searchManagerInstance;
        private HotelManager hotelManager;
        private ILocationStrategy locationStrategy;
        private BookingManager bookingManager;

        private SearchManager(){
            //initialize HotelManager and Location strategy
            hotelManager = HotelManager.GetInstance();
            locationStrategy = new DefaultLocationStrategy(); //by default set to this strategy
            bookingManager = BookingManager.GetInstance();
        }
        public static SearchManager GetInstance(){
            if(searchManagerInstance is null){
                lock(_lock){
                    if(searchManagerInstance is null){
                        searchManagerInstance = new SearchManager();
                    }
                }
            }
            return searchManagerInstance;
        }

        

        

    #region < initial filters for user with filter : dates and location >

        public List<Hotel> FiterHotelsBasedOnLocationAndDate(DateTime checkinDate, DateTime checkoutDate, Location location){
            //get all hotels
            List<Hotel> allHotels = GetHotels();

            //filter hotel based on lcoation
            List<Hotel> filteredHotelsBasedOnLocation = GetHotelsBasedOnLocation(location, allHotels);

            //filter hotel based on dates
            return  GetHotelsBasedOnDateRange(checkinDate, checkoutDate, filteredHotelsBasedOnLocation);

            
        }

        private List<Hotel> GetHotels(){
                return hotelManager.GetHotels();
        }

        private List<Hotel> GetHotelsBasedOnLocation(Location location, List<Hotel> allHotels){
            return locationStrategy.FindHotels(allHotels, location);
        }

    private List<Hotel> GetHotelsBasedOnDateRange(DateTime startDate, DateTime endDate, List<Hotel> filteredHotelsBasedOnLocation)
    {
            var bookings = bookingManager.GetBookings();

            var filteredHotelIds = filteredHotelsBasedOnLocation.Select(h => h.Id).ToHashSet(); // faster lookup
            var bookedHotelIds = bookings
                                    .Where(booking => filteredHotelIds.Contains(booking.HotelId))
                                    .Where(booking => (startDate <= booking.CheckOutDate && endDate >= booking.CheckInDate))
                                    .Select(b => b.HotelId);

            //hotels that are not booked yet  or booked but availble for that date range are available
            var availableHotelIds =filteredHotelIds.Where(id => !bookedHotelIds.Contains(id));

            var availableHotels = availableHotelIds.Select(Id => hotelManager.GetHotel(Id)).ToList();
                                    // you only need ToList() at the end if return type is List<Hotel>
                                    //deferring execution of query till end(tolist is where all query actually gets executed)

            return availableHotels;
    }

        
    #endregion



        public decimal CalculatePrice(int hotelId, List<int> roomIds ,DateTime startDate, DateTime endDate){
            Hotel hotel = hotelManager.GetHotel(hotelId);
            int numberOfDays = (endDate - startDate).Days;
            return roomIds.Aggregate(0.0m,(sum,roomId)=> hotel.GetRoom(roomId).PricePerDay * numberOfDays);
        }


#region < Custom Filter & Sort >
        public List<Hotel> FilterHotelsByUserPreferences(List<Hotel> hotels, HotelFilter hotelFilter, List<UserSortPreference> userSortPreferences){
            List<Hotel>  filteredHotels = FilterHotels(hotels, hotelFilter);
            List<Hotel>  sortedHotels = SortHotels(filteredHotels, userSortPreferences);
            return sortedHotels;
        }

        //Custom Filter
        private List<Hotel> FilterHotels(List<Hotel> hotels, HotelFilter filter)
        {
            var query = hotels.AsEnumerable(); //since we want to exec  all logic at the end, using enumerable  for now

            
            if (filter.MaxPrice.HasValue) //notice since nullable, checking if has value
                query = query.Where(h => h.PricePerNight <= filter.MaxPrice.Value); //get the value of the nullable prop

            if (filter.MinRating.HasValue)
                query = query.Where(h => h.Rating >= filter.MinRating.Value);

            if (filter.RequiredAmenities != null && filter.RequiredAmenities.Any())
                query = query.Where(h =>
                    filter.RequiredAmenities.All(ra => h.Amenities.Contains(ra)));

            return query.ToList(); //query executed
        }


        //Custom sort
        private List<Hotel> SortHotels(List<Hotel> hotels, List<UserSortPreference> preferences)
        {
            IOrderedEnumerable<Hotel> sorted = null;

            foreach (var pref in preferences)
            {
                Func<Hotel, object> keySelector = pref.Field switch
                {
                    SortFieldEnum.Price => h => h.PricePerNight,
                    SortFieldEnum.Rating => h => h.Rating,
                    _ => h => h.Name
                };

                if (sorted == null)
                {
                    sorted = pref.Order == SortOrderEnum.Ascending
                        ? hotels.OrderBy(keySelector)
                        : hotels.OrderByDescending(keySelector);
                }
                else
                {
                    sorted = pref.Order == SortOrderEnum.Ascending
                        ? sorted.ThenBy(keySelector)
                        : sorted.ThenByDescending(keySelector);
                }
            }

            return sorted?.ToList() ?? hotels;
        }

#endregion

    }
}