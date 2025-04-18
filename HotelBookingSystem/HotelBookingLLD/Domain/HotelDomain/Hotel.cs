using HotelBookingLLD.Domain;
namespace HotelBookingLLD.Domain.HotelDomain{
    public class Hotel{
        private static int _idCounter = 0;
        public int Id {get; private set;}
        public string Name {get; set;}
        public Dictionary<int,Room> Rooms {get; set;}= new Dictionary<int, Room>();
        public HotelTypeEnum hotelTypeEnum {get;set;}
        public float Rating {get; set;}
        public HashSet<string> Amenities {get; set;}
        public Location Location {get; set;}
        public TimeSpan CheckInTime {get; set;} = new TimeSpan(14,0,0);
        public TimeSpan CheckOutTime {get; set;} = new TimeSpan(11,0,0);
        public decimal PricePerNight {get; set;} = 400.0m ; //settign a default price for all hotel//can be set via constructor

        public Hotel(string name, HotelTypeEnum hotelTypeEnum, float Rating, HashSet<string> Amenities, Location location){
            
            this.Id = ++_idCounter;
            Name = name;
            this.hotelTypeEnum = hotelTypeEnum;
            this.Rating = Rating;
            this.Amenities = Amenities;
            this.Location = location;
            PricePerNight *= (int)hotelTypeEnum;
        }

        public void AddRoom(Room room){
            this.Rooms.Add(room.Id,room);
        }

        public List<Room> GetRooms(){
            return Rooms.Values.ToList();
        }

        public Room GetRoom(int roomId){
            if(Rooms.ContainsKey(roomId)){
                return Rooms[roomId];
            }
            else{
                Console.WriteLine($"Room with {roomId} not found.");
                return null;
            }
        }

        //other admin tasks
        //Remove Room
        //Update Room Details

    }
}