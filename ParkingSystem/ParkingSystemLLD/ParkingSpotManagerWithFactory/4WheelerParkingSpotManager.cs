using ParkingSystemLLD.ParkingSpotStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD.ParkingSpotManagerWithFactoryWithFactory
{
    public class _4WheelerParkingSpotManager : ParkingSpotManager
    {
        private static readonly object _lock = new object();
        private static _4WheelerParkingSpotManager _4WheelerParkingSpotManagerInstance;
        private _4WheelerParkingSpotManager() : base(new LowestPriceParkingSpotStrategy())
        {
        }
        public static _4WheelerParkingSpotManager Get4WheelerParkingSpotManagerInstance()
        {
            if (_4WheelerParkingSpotManagerInstance is null)
            {
                lock (_lock)
                {
                    if (_4WheelerParkingSpotManagerInstance is null)
                    {
                        _4WheelerParkingSpotManagerInstance = new _4WheelerParkingSpotManager();
                    }
                }
            }
            return _4WheelerParkingSpotManagerInstance;
        }

    }
}
