using HotelBookingLLD.Domain.HotelDomain;
using HotelBookingLLD.Domain;

namespace HotelBookingLLD.LocationStrategy{
    public class DefaultLocationStrategy : ILocationStrategy
    {

        public List<Hotel> FindHotels(List<Hotel> hotels, Location location){
            //black boxing
            return hotels
                .Where((hotel, index) => index % 2 == 0) //some random logic to take the every alternate indexed hotels
                .ToList();
            
        }
    }
}