using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow_LLD.Domain.Enum
{
    public class SeatType
    {

        public SeatTypeEnum SeatCategory { get; set; }
        public int Price { get; set; }
        public SeatType(SeatTypeEnum seatCategory, int price)
        {
            SeatCategory = seatCategory;
            Price = price;
        }
    }
}
