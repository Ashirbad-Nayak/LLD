using ElevatorSystemLLD.Domain.Enum;
using ElevatorSystemLLD.LiftSelectionStrategy.WithDestination;
using ElevatorSystemLLD.LiftSelectionStrategy.WithDirection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystemLLD
{
    public class SourceFloor
    {
        private static int _idCounter = 1;
        public int Id { get; }
        private ElevatorController elevatorController;
        private LiftSelectionStrategyWithDestination elevatorSelectionStrategy;
        private LiftSelectionStrategyWithDirection elevatorSelectionStrategyWithDirection;
        public SourceFloor() {
            Id = ++_idCounter;
            IntializeElevatorController();
            InitializeElevatorSelectionStrategy();
        }

        public void InitializeElevatorSelectionStrategy()
        {
            elevatorSelectionStrategy = new NearestLiftSelectionStrategy(elevatorController);
            elevatorSelectionStrategyWithDirection = new DirectionBasedNearestLiftSelectionStrategy(elevatorController);
            //hard coded, can be moved to simple factory and choose strategy bassed on any enum or business logic

        }

        public void IntializeElevatorController()
        {
             elevatorController = ElevatorController.GetInstance();
        }

        public void SelectWithDestination(int sourceFloorId, int destinationFloorId)
        {
            if (elevatorController.GetElevatorCars().Count == 0)
            {
                Console.WriteLine("No Elevators Available");
                return;
            }
            int elevatorId = elevatorSelectionStrategy.SelectElevator(sourceFloorId, destinationFloorId);
            if (elevatorId == -1)
            {
                Console.WriteLine("No Elevators Available");
                return;
            }
            elevatorController.Move(elevatorId, sourceFloorId, destinationFloorId, sourceFloorId > destinationFloorId ? Direction.DOWN: Direction.UP);
        }

        public int SelectWithDirection(int sourceFloorId, Direction direction)
        {
            if (elevatorController.GetElevatorCars().Count == 0)
            {
                Console.WriteLine("No Elevators Available");
                return -1;
            }
            int elevatorId = elevatorSelectionStrategyWithDirection.SelectElevator(sourceFloorId, direction);
            if (elevatorId == -1)
            {
                Console.WriteLine("No Elevators Available");
                return -1;
            }
            return elevatorId;
        }
        public void SelectDestinationFromInsideLift(int sourceFloorId, int destinationFloorId, int elevatorId)
        {
            
            if (elevatorController.ElevatorCars.ContainsKey(elevatorId))
            {
                elevatorController.Move(elevatorId, sourceFloorId, destinationFloorId, sourceFloorId > destinationFloorId ? Direction.DOWN : Direction.UP);
            }
            else
            {
                Console.WriteLine("Invalid Elevator Id");
            }
        }

    }
}
