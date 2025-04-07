using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD.Domain
{
    public class Screen
    {
        private static int _idCounter = 0;
        public int Id { get; }
        public List<Seat> seats { get; set; }
        public Screen()
        {
            Id = ++_idCounter;
            seats = new List<Seat>();
        }
        public void AddSeats(List<Seat> seats)
        {
            this.seats.AddRange(seats);
        }
        public List<Seat> GetAvailableSeats()
        {
            return seats.Where(seat => seat.SeatStatus == Domain.Enum.SeatStatusTypeEnum.AVAILABLE).ToList();
        }
    }
}
