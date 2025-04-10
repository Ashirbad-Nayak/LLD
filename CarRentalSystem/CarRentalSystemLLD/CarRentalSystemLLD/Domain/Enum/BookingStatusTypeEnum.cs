using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystemLLD.Domain.Enum
{
    public enum BookingStatusTypeEnum
    {
        PENDING,
        CONFIRMED, 
        CANCELLED,
        COMPLETED, //only after car is returned
        NO_SHOW,
        REFUNDED,
    }
}
