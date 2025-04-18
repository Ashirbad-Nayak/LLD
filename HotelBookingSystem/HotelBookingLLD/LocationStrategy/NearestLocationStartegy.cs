using HotelBookingLLD.Domain.HotelDomain;
using HotelBookingLLD.Domain;

namespace HotelBookingLLD.LocationStrategy{
    public class NearestLocationStartegy : ILocationStrategy
    {

        public List<Hotel> FindHotels(List<Hotel> hotels, Location location){
            return hotels
                .OrderBy(hotel => CalculateDistance(location, hotel.Location))
                .Take(3)
                .ToList();
            
            double CalculateDistance(Location loc1, Location loc2)
            {
                //black boxing
                Random random = new Random();
                double incrementalDist = random.NextDouble() * (100.0 - 1.0) + 1.0;
                return incrementalDist;
                //return Math.Sqrt(Math.Pow(loc1.Latitude - loc2.Latitude, 2) + Math.Pow(loc1.Longitude - loc2.Longitude, 2));
            }
        }
    }
}