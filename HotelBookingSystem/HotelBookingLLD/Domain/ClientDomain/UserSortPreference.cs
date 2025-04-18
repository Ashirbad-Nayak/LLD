using HotelBookingLLD.Domain.ClientDomain.ClientDomainEnum;
namespace HotelBookingLLD.Domain.ClientDomain{
    public class UserSortPreference{
        public SortFieldEnum Field {get;set;}
        public SortOrderEnum Order {get;set;}
    }
}