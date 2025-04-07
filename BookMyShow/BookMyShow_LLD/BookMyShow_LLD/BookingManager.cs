using BookMyShow_LLD.Domain;
using BookMyShow_LLD.Domain.Enum;
using BookMyShow_LLD.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD
{
    public class BookingManager
    {
        private static readonly object _lock = new object();
        private static BookingManager _instance;
        public Dictionary<int, Booking> Bookings { get; set; } = new Dictionary<int, Booking>();
        private IPaymentStrategy paymentStrategy;
        private BookingManager()
        {
            // Private constructor to prevent instantiation from outside
        }
        public static BookingManager Instance
        {
            get
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
        }
        public int CreateReservation_Booking(int showId, List<Seat> seats)
        {
            //validate seatstatus
            foreach (var seat in seats)
            {
                if (seat.SeatStatus != SeatStatusTypeEnum.AVAILABLE)
                {
                   Console.WriteLine($"Seat {seat.Id} is not available for booking,Cancelling entire booking");
                    return -1;
                }
            }
            double totalPrice = seats.Aggregate(0.0, (sum, seat) => sum + seat.seatType.Price);
            Console.WriteLine($"Show: {showId} ,Total price for booking: {totalPrice}");
            Booking booking = new Booking(showId,seats, totalPrice);
            Bookings.Add(booking.Id, booking);
            return booking.Id;
        }

        //Additional logic not implemented: maybe maintain another map<booking id , paymentstatus>
        //atevery step starting from creating booking till end, maintian a status
        //before proceeding to payment, check if booking is still valid
        //before cancelling booking, check if payment is in progress/already done/cancelled
        public void CancelBooking(int bookingId)
        {
            if (Bookings.ContainsKey(bookingId))
            {
                Booking booking = Bookings[bookingId];
                booking.UpdateStatus(BookingStatusEnum.CANCELLED);
                Bookings.Remove(bookingId);
            }
            else
            {
                throw new Exception("Booking not found");
            }
        }
        public void FillPaymentDetailsAndMakePayment(int bookingId, string paymentType)
        {
            if (Bookings.ContainsKey(bookingId))
            {
                Booking booking = Bookings[bookingId];
                switch (paymentType)
                {
                    case "CreditCard":
                        paymentStrategy = new CreditCardPaymentStrategy();
                        break;
                    case "UPI":
                        paymentStrategy = new UPIPaymentStrategy();
                        break;
                    default:
                        throw new Exception("Invalid payment type");
                }
               
                //ideally payment is 3rd party
                //also i wan tuser 2 to be able to see avaialble seats while this payment is in progress
                //hence, assigning task to one of the thread in threadpool
                Task.Run(() =>
                {
                    bool paymentSucceded = paymentStrategy.ProcessPayment(booking.Id, Bookings[bookingId].TotalPrice);
                    Task.Delay(5000).Wait(); // Simulate payment processing time
                    UpdateBookingStatus(bookingId, paymentSucceded);
                });
               
            }
            else
            {
                throw new Exception("Booking not found");
            }

        }

        //ideally this is trigger via webhook fomr payment system once payment is completed
       private void UpdateBookingStatus(int bookingId, bool paymentSucceeded)
        {
            if (Bookings.ContainsKey(bookingId))
            {
                Booking booking = Bookings[bookingId];
                if (paymentSucceeded)
                {

                    Console.WriteLine("Payment successful!");
                    booking.UpdateStatus(BookingStatusEnum.CONFIRMED);
                }
                else
                {
                    Console.WriteLine("Payment failed.");
                    booking.UpdateStatus(BookingStatusEnum.CANCELLED);
                }
            }
            else
            {
                throw new Exception("Booking not found");
            }
        }
    }
}
