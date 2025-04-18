using HotelBookingLLD.Domain.HotelDomain;
using HotelBookingLLD.Domain;

namespace HotelBookingLLD.LocationStrategy{
    public interface ILocationStrategy{
        public List<Hotel> FindHotels(List<Hotel> hotels, Location location);
    }
}