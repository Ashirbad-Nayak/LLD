using BookMyShow_LLD.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD.Domain
{
    public class Booking
    {
        private static int _idCounter = 0;
        public int Id { get; }

        public int ShowId { get; }
        public List<Seat> BookedSeats { get;} = new List<Seat>();
        public double TotalPrice { get; }
        public BookingStatusEnum BookingStatus { get; set; }

        
        public Booking(int showId, List<Seat> bookedSeats, double totalPrice)
        {
            Id = ++_idCounter;
            ShowId = showId;
            BookedSeats = bookedSeats;
            TotalPrice = totalPrice;
            BookingStatus = BookingStatusEnum.PENDING;
            //update seats
            UpdateSeatStatus(SeatStatusTypeEnum.BLOCKED);
        }

        private void UpdateSeatStatus(SeatStatusTypeEnum seatStatus)
        {
            foreach (var seat in BookedSeats)
            {
                seat.UpdateSeatStatus(seatStatus);
            }
        }
        public void UpdateStatus(BookingStatusEnum bookingStatus)
        {
            //update seats
            if(bookingStatus == BookingStatusEnum.CONFIRMED)
            {
                UpdateSeatStatus(SeatStatusTypeEnum.BOOKED);
                Console.WriteLine($"Booking {Id} confirmed");
            }
            else if (bookingStatus == BookingStatusEnum.CANCELLED)
            {
                UpdateSeatStatus(SeatStatusTypeEnum.AVAILABLE);
                Console.WriteLine($"Booking {Id} cancelled");
            }
                

            BookingStatus = bookingStatus;
        }



    }
}
