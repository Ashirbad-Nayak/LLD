
using HotelBookingLLD.Domain.BookingDomain.BookingEnum;

namespace HotelBookingLLD.Domain.BookingDomain{
    public class Booking{
        private static int _idCounter = 1;
        public int Id {get; private set;}
        public DateTime CheckInDate {get; private set;}
        public DateTime CheckOutDate {get; private set;}
        public decimal Price {get; private set;}
        private BookingStatusTypeEnum bookingStatus {get; set;} 
        private List<int> roomIds {get; set;} =new List<int>();
        public int HotelId {get; private set;}

        public Booking(DateTime CheckInDate, DateTime CheckOutDate, decimal price, List<int> roomIds,int hotelId){
            Id = _idCounter++;
            this.roomIds = roomIds;
            HotelId = hotelId;
            this.CheckInDate = CheckInDate;
            this.CheckOutDate = CheckOutDate;
            Price = price;
            bookingStatus = BookingStatusTypeEnum.PENDING;
        }

        public void UpdateBookingStatus(BookingStatusTypeEnum bookingStatus){
            this.bookingStatus = bookingStatus;
            Console.WriteLine($"Booking {Id} Status updated to {bookingStatus.ToString()}.");
        }
        



    }
}