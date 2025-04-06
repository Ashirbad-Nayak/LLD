using ElevatorSystemLLD.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystemLLD
{
    public class ElevatorController
    {
        private static readonly Object _lock = new Object();
        private static ElevatorController _instance;
        public Dictionary<int, ElevatorCar> ElevatorCars { get; set; } = new Dictionary<int, ElevatorCar>();
        private ElevatorController()
        {
        }
        public static ElevatorController GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new ElevatorController();
                    }
                }
            }
            return _instance;
        }
        public void AddElevatorCar(ElevatorCar elevatorCar)
        {
            if (elevatorCar != null)
            {
                ElevatorCars.Add(elevatorCar.Id, elevatorCar);
            }
        }
        public void RemoveElevatorCar(int elevatorId)
        {
            if (ElevatorCars.ContainsKey(elevatorId))
            {
                ElevatorCars.Remove(elevatorId);
            }
        }
        public List<ElevatorCar> GetElevatorCars()
        {
           return ElevatorCars.Values.ToList();
        }
        public void Move(int elevatorId, int sourceFloorId, int destinationFloorId, Direction direction)
        {
            if (ElevatorCars.ContainsKey(elevatorId) &&
                ElevatorCars[elevatorId].ValidateSourceAndDestinationFloor(sourceFloorId, destinationFloorId))
            {
                ElevatorCars[elevatorId].UpdateHeap(sourceFloorId, destinationFloorId, direction);
            }
        }

    }
}
