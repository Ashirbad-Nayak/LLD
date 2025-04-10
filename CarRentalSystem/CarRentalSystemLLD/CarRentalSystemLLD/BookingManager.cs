using BookMyShow_LLD.Payment;
using CarRentalSystemLLD.Domain;
using CarRentalSystemLLD.Domain.Enum;
using CarRentalSystemLLD.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystemLLD
{
    public class BookingManager
    {
        private CarManager carManager { get; set; }
        private IPaymentStrategy paymentStrategy { get; set; }
        private static readonly Object _lock = new object();
        private static BookingManager _instance;
        public Dictionary<int, Booking> Bookings { get; set; } = new Dictionary<int, Booking>();
        private BookingManager()
        {
            //private constructor to prevent instantiation
            carManager = CarManager.GetInstance();
        }
        public static BookingManager GetInstance()
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new BookingManager();
                }
            }
            return _instance;
        }
        public List<Booking> GetAllBookings()
        {
            return Bookings.Values.ToList();
        }
        public int CreateBooking(Booking booking)
        {
            
            if (Bookings.ContainsKey(booking.BookingId))
            {
                Console.WriteLine($"Booking with ID {booking.BookingId} already exists.");
                return -1;
            }
            //calculate total price
            booking.TotalPrice = carManager.CalculatePrice(booking.CarId, booking.StartDate.ToString(), booking.EndDate.ToString());

            Bookings.Add(booking.BookingId, booking);
            //update car avaialbility
            carManager.UpdateCarAvailability(booking.CarId, false);
            
            Console.WriteLine($"Booking with ID {booking.BookingId} created successfully.");
            booking.DisplayBookingDetails();

            return booking.BookingId;
        }
        public void CancelBooking(int bookingId)
        {
            if (!Bookings.ContainsKey(bookingId))
            {
                Console.WriteLine($"Booking with ID {bookingId} not found.");
                return;
            }
            Booking booking = Bookings[bookingId];
            //update car availability
            carManager.UpdateCarAvailability(booking.CarId, true);
            //update booking status
            booking.UpdateBookingStatus(BookingStatusTypeEnum.CANCELLED);
        }
        public void MakePayment(int bookingid,string paymentType)
        {
            if (!Bookings.ContainsKey(bookingid))
            {
                Console.WriteLine($"Booking with ID {bookingid} not found.");
                return;
            }
            Booking booking = Bookings[bookingid];

            //switch between payment based on payment type
            switch (paymentType)
            {
                case "CASH":
                    paymentStrategy = new CashPaymentStrategy();
                    break;
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

            //make payment
            Task.Run(() =>
            {
                bool ispaymentSuccess = paymentStrategy.ProcessPayment(bookingid, booking.TotalPrice);
                Task.Delay(5000).Wait(); // Simulate delay for payment processing
                UpdateBookingStatus(bookingid, ispaymentSuccess);
            });
            

        }
        public void UpdateBookingStatus(int bookingid,bool paymentSuccess)
        {
            if (!Bookings.ContainsKey(bookingid))
            {
                Console.WriteLine($"Booking with ID {bookingid} not found.");
                return;
            }
            Booking booking = Bookings[bookingid];
            if (paymentSuccess)
            {
                booking.UpdateBookingStatus(BookingStatusTypeEnum.CONFIRMED);
                Console.WriteLine($"Payment successful for booking ID {bookingid}. Booking confirmed.");
            }
            else
            {
                booking.UpdateBookingStatus(BookingStatusTypeEnum.CANCELLED);
                Console.WriteLine($"Payment failed for booking ID {bookingid}. Booking status updated to FAILED.");
            }

            //update car avaialbility
            carManager.UpdateCarAvailability(booking.CarId, true);
        }



    }
}
