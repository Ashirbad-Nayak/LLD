using HotelBookingLLD.Domain.HotelDomain;
namespace HotelBookingLLD{
    public class HotelManager{
        private static readonly Object _lock = new Object();
        private static HotelManager _hotelManagerInstance;
        public Dictionary<int, Hotel> hotels {get;set;} = new Dictionary<int, Hotel>();

        private HotelManager(){
            //private constructor to prevent multiple instantiation
        }

        public static HotelManager GetInstance(){
            if(_hotelManagerInstance is null){
                lock(_lock){
                    if(_hotelManagerInstance is null){
                        _hotelManagerInstance = new HotelManager();
                    }
                }
            }
            return _hotelManagerInstance;
        }
        

        public void AddHotel(Hotel hotel){
            hotels.Add(hotel.Id, hotel);
        }

        public List<Hotel> GetHotels(){
            return hotels.Values.ToList();
        }

        public Hotel GetHotel(int hotelId){
            if(hotels.ContainsKey(hotelId))
            return hotels[hotelId];
            else{
                Console.WriteLine($"Hotel with  {hotelId} does not exist.");
                return null;
            }
        }

        public Room GetHotelRoom(int hotelId, int roomId){
            if(hotels.ContainsKey(hotelId) && hotels[hotelId].Rooms.ContainsKey(roomId))
            return hotels[hotelId].Rooms[roomId];
            else{
                Console.WriteLine($"Hotel with  {hotelId} and room - {roomId} does not exist.");
                return null;
            }
        }

        //other admin methods
        //remove
        //Update
    }
}