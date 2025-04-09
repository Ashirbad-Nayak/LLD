using ParkingSystemLLD.ParkingSpotStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystemLLD.ParkingSpotManagerWithFactoryWithFactory
{
    public class _2WheelerParkingSpotManager : ParkingSpotManager
    {
        private static readonly object _lock = new object();
        private static _2WheelerParkingSpotManager _2WheelerParkingSpotManagerInstance;
        private _2WheelerParkingSpotManager() : base(new NearExitParkingSpotStrategy())
        {
        }
        public static _2WheelerParkingSpotManager Get2WheelerParkingSpotManagerInstance()
        {
            if (_2WheelerParkingSpotManagerInstance is null)
            {
                lock (_lock)
                {
                    if (_2WheelerParkingSpotManagerInstance is null)
                    {
                        _2WheelerParkingSpotManagerInstance = new _2WheelerParkingSpotManager();
                    }
                }
            }
            return _2WheelerParkingSpotManagerInstance;
        }

    }
}
