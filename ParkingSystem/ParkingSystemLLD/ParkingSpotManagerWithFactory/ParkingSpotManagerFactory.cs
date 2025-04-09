using ParkingSystemLLD.Domain.Enums;
using ParkingSystemLLD.ParkingSpotManagerWithFactoryWithFactory;
using ParkingSystemLLD.ParkingSpotStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD.ParkingSpotManagerWithFactory
{
    public class ParkingSpotManagerFactory
    {
        public  static ParkingSpotManager GetParkingSpotManager(ParkingSpotTypeEnum parkingSpotType)
        {
            switch (parkingSpotType)
            {
                case ParkingSpotTypeEnum.TwoWheeler:
                    return  _2WheelerParkingSpotManager.Get2WheelerParkingSpotManagerInstance();
                case ParkingSpotTypeEnum.FourWheeler:
                    return  _4WheelerParkingSpotManager.Get4WheelerParkingSpotManagerInstance();
                default:
                    return new ParkingSpotManager(new LowestPriceParkingSpotStrategy());
            }
        }

    }
}
