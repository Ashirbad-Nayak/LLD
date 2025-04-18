using HotelBookingLLD.Domain;
using HotelBookingLLD.Domain.HotelDomain;
using HotelBookingLLD.Domain.ClientDomain;
using HotelBookingLLD.Domain.ClientDomain.ClientDomainEnum;
using HotelBookingLLD.Domain.BookingDomain;
using HotelBookingLLD.Domain.BookingDomain.BookingEnum;


namespace HotelBookingLLD{
    public class HotelBookingProcessor{
        private static readonly Object _lock = new Object();
        private static HotelBookingProcessor hotelBookingProcessorInstance;
        private HotelManager hotelManager;
        private SearchManager searchManager;
        private BookingManager bookingManager;

        private HotelBookingProcessor (){
            //initialize
            hotelManager = HotelManager.GetInstance();
            searchManager = SearchManager.GetInstance();
            bookingManager = BookingManager.GetInstance();
        }

        public static HotelBookingProcessor  GetInstance(){
            if(hotelBookingProcessorInstance is null ){
                lock(_lock){
                    if(hotelBookingProcessorInstance is null){
                        hotelBookingProcessorInstance = new HotelBookingProcessor();
                    }
                }
            }
            return hotelBookingProcessorInstance;
        }

        #region < Hotel Manager>

        public void AddHotel(Hotel hotel){
            hotelManager.AddHotel(hotel);
        }

        public List<Hotel> GetHotels(){
            return hotelManager.GetHotels();
        }

        public Hotel GetHotel(int hotelId){
            return hotelManager.GetHotel(hotelId);
        }

        public Room GetHotelRoom(int hotelId, int roomId){
            return hotelManager.GetHotelRoom(hotelId, roomId);
        }

        #endregion

        #region <Search Manager>

        public List<Hotel> FiterHotelsBasedOnLocationAndDate(DateTime checkinDate, DateTime checkoutDate, Location location){
            return searchManager. FiterHotelsBasedOnLocationAndDate(checkinDate, checkoutDate, location);
        }

        public List<Hotel> FilterHotelsByUserPreferences(List<Hotel> hotels, HotelFilter hotelFilter, List<UserSortPreference> userSortPreferences){
            return searchManager.FilterHotelsByUserPreferences(hotels, hotelFilter, userSortPreferences);
        }

        public decimal CalculatePrice(int hotelId, List<int> roomIds ,DateTime startDate, DateTime endDate){
            return searchManager.CalculatePrice(hotelId, roomIds, startDate, endDate);
        }

        #endregion

        #region <Booking Manager>
        public int CreateBooking(DateTime checkInDate, DateTime checkOutDate, int hotelId, List<int> roomIds){
            return bookingManager.CreateBooking(checkInDate, checkOutDate, hotelId, roomIds);
        }

        public void MakePayment(int bookingId, string paymentType){
             bookingManager.MakePayment(bookingId, paymentType);
        }
        #endregion

    }
}
