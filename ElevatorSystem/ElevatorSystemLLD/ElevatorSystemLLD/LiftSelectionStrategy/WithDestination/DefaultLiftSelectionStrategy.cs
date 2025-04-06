using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystemLLD.LiftSelectionStrategy.WithDestination
{
    public class DefaultLiftSelectionStrategy : LiftSelectionStrategyWithDestination
    {
        public DefaultLiftSelectionStrategy(ElevatorController elevatorController) : base(elevatorController)
        {
        }


        public override int SelectElevator(int sourceFloorId, int destinationFloorId)
        {
            List<ElevatorCar> elevatorCars = elevatorController.GetElevatorCars();
            if (elevatorCars == null || elevatorCars.Count == 0)
            {
                Console.WriteLine("No elevators available");
                return -1; // No elevators available
            }
            // Logic to select the elevator based on some default criteria
            // This is a placeholder implementation and should be replaced with actual logic

            return 0; // Return the ID of the selected elevator
        }
    }
}
