using BookMyShow_LLD.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD.Domain
{
    public class Seat
    {
        private static int _idCounter =0;
        public int Id { get; }
        public int Row { get; set; }
        public SeatType seatType { get; set; } // SeatTypeEnum with price
        public SeatStatusTypeEnum SeatStatus { get; set; }

        public Seat()
        {
            Id = ++_idCounter;
            SeatStatus = SeatStatusTypeEnum.AVAILABLE;
        }

        public void UpdateSeatStatus(SeatStatusTypeEnum seatStatus)
        {
            SeatStatus = seatStatus;
            Console.WriteLine($"Seat {Id} status updated to {SeatStatus}");
            //if status is blicked/reserved
            //initiate a timer to auto realease the seat i.e. update the status back to available only if status is not booked
        }


    }
}
