using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystemLLD.LiftSelectionStrategy.WithDestination
{
    public class NearestLiftSelectionStrategy : LiftSelectionStrategyWithDestination
    {
        public NearestLiftSelectionStrategy(ElevatorController elevatorController) : base(elevatorController)
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

            // Logic to select the nearest elevator based on the source and destination floor IDs
            // This is a placeholder implementation and should be replaced with actual logic
            Random random = new Random();
            int randomIndex = random.Next(0, elevatorCars.Count);
            Console.WriteLine($"Selected Elevator: {elevatorCars[randomIndex].Id}");
            return randomIndex; // Return the ID of the selected elevator
        }
    }
}
