
namespace HotelBookingLLD.Domain.HotelDomain{
    public class Room{
        private static int _idCounter = 0;
        public int Id {get; private set;}
        public decimal PricePerDay {get;set;}
        public int MaxOccupancy{get;set;}
        public RoomTypeEnum RoomType {get; set;}

        public Room(decimal price, int max, RoomTypeEnum roomType){
            Id = _idCounter++;
            PricePerDay = price;
            MaxOccupancy = max;
            RoomType = roomType;
        }
    }
}