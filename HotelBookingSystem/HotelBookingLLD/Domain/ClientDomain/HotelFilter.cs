
namespace HotelBookingLLD.Domain.ClientDomain{
    public class HotelFilter{
        public decimal? MaxPrice {get;set;}
        public float? MinRating {get; set;}
        public List<string> RequiredAmenities {get; set;}
    }
}