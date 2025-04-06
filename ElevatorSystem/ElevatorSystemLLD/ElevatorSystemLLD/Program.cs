using ElevatorSystemLLD.Domain.Enum;

namespace ElevatorSystemLLD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //create elevators
            List<ElevatorCar> elevatorCars = CreateElevators();

            //create elevator controller and add elevators
            ElevatorController elevatorController = ElevatorController.GetInstance();
            foreach (ElevatorCar elevatorCar in elevatorCars)
            {
                elevatorController.AddElevatorCar(elevatorCar);
            }


            //create floors
            List<SourceFloor> sourceFloors = CreateFloors();

            Random random = new Random();
            //foreach floor add multiple req to move
            foreach (SourceFloor sourceFloor in sourceFloors)
            {
              
                    //User selects destination floor from outside lift/from floor
                    int destinationFloorId = random.Next(1, sourceFloors.Count);
                    Console.WriteLine($"User wants to move from floor {sourceFloor.Id} to floor {destinationFloorId}");
                    sourceFloor.SelectWithDestination(sourceFloor.Id, destinationFloorId);
                
            }

            Task.Delay(30000).Wait();

            //foreach floor add multiple req to move
            foreach (SourceFloor sourceFloor in sourceFloors)
            {
                
                
                    //User selects direction from outside lift/from floor
                    Direction direction = random.Next(0,1)==0 ? Direction.UP : Direction.DOWN;
                    Console.WriteLine($"User wants to move from floor {sourceFloor.Id} in direction {direction}");

                    int elevatorId = sourceFloor.SelectWithDirection(sourceFloor.Id, direction);
                    Console.WriteLine("Use should go near elevator " + elevatorId);

                    //User selects destination floor from inside elevator car
                    int destinationFloorId = random.Next(1, sourceFloors.Count);
                    Console.WriteLine($"User wants to move from floor {sourceFloor.Id} to floor {destinationFloorId}");

                    sourceFloor.SelectDestinationFromInsideLift(sourceFloor.Id, destinationFloorId, elevatorId);
                
            }
            //verify the order and elevator Id
            Task.Delay(55000).Wait();
        }

        public static List<ElevatorCar> CreateElevators()
        {
            List<ElevatorCar> elevatorCars = new List<ElevatorCar>();
            for (int i = 0; i < 2; i++)//first 1 car only and verify logic//it works well with multiple cars
            {
                ElevatorCar elevatorCar = new ElevatorCar();
                elevatorCar.AddBlackListedFloor(5);//hardcoding no one should be able to go to 5th floor
                elevatorCars.Add(elevatorCar);
            }
            return elevatorCars;
        }
        public static List<SourceFloor> CreateFloors()
        {
            List<SourceFloor> sourceFloors = new List<SourceFloor>();
            for (int i = 1; i < 10; i++)
            {
                SourceFloor sourceFloor = new SourceFloor();
                sourceFloors.Add(sourceFloor);
            }
            return sourceFloors;
        }
    }
}
