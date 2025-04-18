using HotelBookingLLD.Domain.BookingDomain;
using HotelBookingLLD.Domain.BookingDomain.BookingEnum;
using HotelBookingLLD.Payment;

namespace HotelBookingLLD{
    public class BookingManager{
        private static readonly Object _lock = new Object();
        private static BookingManager bookingManagerInstance;
        private SearchManager searchManager;
        private IPaymentStrategy paymentStrategy;
        private Dictionary<int,Booking> bookings {get;set;} = new Dictionary<int,Booking>();

        private BookingManager(){
        
        }

        public static BookingManager GetInstance(){
            if(bookingManagerInstance is null){
                lock(_lock){
                    if(bookingManagerInstance is null){
                        bookingManagerInstance = new BookingManager();
                    }
                }
            }
            return bookingManagerInstance;
        }

        public void InitializeSearchManager(){
            searchManager = SearchManager.GetInstance();//need for price calculation
        }


        //Create Booking
        public int CreateBooking(DateTime checkInDate, DateTime checkOutDate, int hotelId, List<int> roomsIds){
            //calculate price
            InitializeSearchManager();
            decimal totalPrice = searchManager.CalculatePrice(hotelId, roomsIds, checkInDate, checkOutDate);
            Booking booking = new Booking(checkInDate, checkOutDate, totalPrice, roomsIds, hotelId);
            Console.WriteLine($"Booking Created with booking id - {booking.Id}. User to pay amount : {totalPrice}");

            bookings.Add(booking.Id,booking);
            return booking.Id;
            
        }

        public List<Booking> GetBookings()
        {
            return bookings.Values.ToList();
        }

        public Booking GetBooking(int bookingId){
            if(bookings.ContainsKey(bookingId))
            return bookings[bookingId];
            else{
                Console.WriteLine($"Booking with ID {bookingId} not found.");
                return null;
            }
        }


        public void MakePayment(int bookingId, string paymentType){
            if (!bookings.ContainsKey(bookingId))
            {
                Console.WriteLine($"Booking with ID {bookingId} not found.");
                return;
            }
            Booking booking = bookings[bookingId];

            //switch between payment based on payment type
            switch (paymentType)
            {
                case "CARD":
                    paymentStrategy = new CreditCardPaymentStrategy();
                    break;
                case "UPI":
                    paymentStrategy = new UPIPaymentStrategy();
                    break;
                default:
                    Console.WriteLine("Invalid payment type");
                    return;
            }
            bool ispaymentSuccess = paymentStrategy.ProcessPayment(bookingId, booking.Price);
            Task.Delay(1000).Wait(); // Simulate delay for payment processing
            UpdateBookingStatus(bookingId, ispaymentSuccess);
        }

        private void UpdateBookingStatus(int bookingId, bool isPaymentSuccess){
            Booking booking = bookings[bookingId];;
            if(isPaymentSuccess){
                booking.UpdateBookingStatus(BookingStatusTypeEnum.BOOKED);
            }else{

                booking.UpdateBookingStatus(BookingStatusTypeEnum.CANCELLED);//conider canceleld if payment fails
            }

        }


    }
}