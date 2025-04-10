using CarRentalSystemLLD.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystemLLD.Domain
{
    public class Booking
    {
        private static int _bookingIdCounter = 0;
        public int BookingId { get; private set; }
        public int CarId { get; private set; }
        public int UserId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public double TotalPrice { get;  set; }
        public string PickupLocation { get; set; }
        public string DropLocation { get; set; }
        public BookingStatusTypeEnum BookingStatusType { get; set; }
        public Booking(int carId, int userId, DateTime startDate, DateTime endDate, string pickupLocation, string dropLocation)
        {
            BookingId = _bookingIdCounter++;
            CarId = carId;
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
            PickupLocation = pickupLocation;
            DropLocation = dropLocation;
            BookingStatusType = BookingStatusTypeEnum.PENDING; // Default status
        }
        public void UpdateBookingStatus(BookingStatusTypeEnum status)
        {
            BookingStatusType = status;
        }

        public void DisplayBookingDetails()
        {
            Console.WriteLine($"Booking ID: {BookingId}, Car ID: {CarId}, User ID: {UserId}");
            Console.WriteLine($">>> Start Date: {StartDate}, End Date: {EndDate}");
            Console.WriteLine($">>> Total Price: {TotalPrice}, Pickup Location: {PickupLocation}, Drop Location: {DropLocation}");
            Console.WriteLine($">>> Booking Status: {BookingStatusType.ToString()}");
        }

    }
}
