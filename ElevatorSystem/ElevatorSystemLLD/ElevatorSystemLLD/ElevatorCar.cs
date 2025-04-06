using ElevatorSystemLLD.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystemLLD
{
    public class ElevatorCar
    {
        private static int _idCounter =1;

        public int Id { get; }

        private int _currentFloor = 0;
        public int MaxFloorId { get; set; }
        public int MinFloorId { get; set; }
        public HashSet<int> BlackListedFloors { get; set; }
        public PriorityQueue<int, int> Up{ get; set; } = new PriorityQueue<int, int>();
        public PriorityQueue<int, int> Down { get; set; } = new PriorityQueue<int, int>();
        public HashSet<int> YetToStop { get; set; } = new HashSet<int>();
        public HashSet<int> UpSet { get; set; } = new HashSet<int>();
        public HashSet<int> DownSet { get; set; } = new HashSet<int>();
        public StatusEnum Status { get; set; }

        public ElevatorCar()
        {
            Id = _idCounter++;
            Status = StatusEnum.IDLE;
        }
        public bool ValidateSourceAndDestinationFloor(int sourceFloor, int destinationFloor)
        {
            if(MinFloorId != 0 && MaxFloorId != 0)
            {
                if (sourceFloor < MinFloorId || sourceFloor > MaxFloorId)
                {
                    Console.WriteLine("Invalid Source Floor");
                    //throw new Exception("Invalid Source Floor");
                }
                if (destinationFloor < MinFloorId || destinationFloor > MaxFloorId)
                {
                    Console.WriteLine("Invalid Destination Floor");
                   // throw new Exception("Invalid Destination Floor");
                }
                return false;
            }
            if (BlackListedFloors.Contains(sourceFloor) || BlackListedFloors.Contains(destinationFloor))
            {
                    Console.WriteLine($"Elevator - {Id} does not go to {sourceFloor} or {destinationFloor}.Please try a different Elevator");
                //throw new Exception("Blacklisted Floor");
                return false;
            }

            return true;
           
        }
        public void AddBlackListedFloor(int floor)
        {
            if (BlackListedFloors == null)
            {
                BlackListedFloors = new HashSet<int>();
            }
            BlackListedFloors.Add(floor);
        }
        public void RemoveBlackListedFloor(int floor)
        {
            if (BlackListedFloors == null)
            {
                BlackListedFloors = new HashSet<int>();
            }
            BlackListedFloors.Remove(floor);
        }

        public void UpdateHeap(int sourceFloorId, int destinationFloorId, Direction direction)
        {

            if (direction == Direction.UP && (Status == StatusEnum.MOVING_UP || Status == StatusEnum.IDLE))
            {
                if (sourceFloorId < _currentFloor)
                {
                    if (!DownSet.Contains(sourceFloorId))
                    {
                        Down.Enqueue(sourceFloorId, -sourceFloorId);
                        DownSet.Add(sourceFloorId);
                    }
                    YetToStop.Add(destinationFloorId);
                }
                else
                {
                    if (!UpSet.Contains(sourceFloorId))
                    {
                        Up.Enqueue(sourceFloorId, sourceFloorId);
                        UpSet.Add(sourceFloorId);
                    }
                    if (!UpSet.Contains(destinationFloorId))
                    {
                        Up.Enqueue(destinationFloorId, destinationFloorId);
                        UpSet.Add(destinationFloorId);
                    }
                }
            }
            else if (direction == Direction.DOWN && (Status == StatusEnum.MOVING_DOWN || Status == StatusEnum.IDLE))
            {
                if (sourceFloorId > _currentFloor)
                {
                    if (!UpSet.Contains(sourceFloorId))
                    {
                        Up.Enqueue(sourceFloorId, sourceFloorId);
                        UpSet.Add(sourceFloorId);
                    }
                    YetToStop.Add(destinationFloorId);
                }
                else
                {
                    if (!DownSet.Contains(sourceFloorId))
                    {
                        Down.Enqueue(sourceFloorId, -sourceFloorId);
                        DownSet.Add(sourceFloorId);
                    }
                    if (!DownSet.Contains(destinationFloorId))
                    {
                        Down.Enqueue(destinationFloorId, -destinationFloorId);
                        DownSet.Add(destinationFloorId);
                    }
                }
            }
            else if (direction == Direction.UP && (Status == StatusEnum.MOVING_DOWN))
            {
                if (sourceFloorId < _currentFloor)
                {
                    if (!DownSet.Contains(sourceFloorId))
                    {
                        Down.Enqueue(sourceFloorId, -sourceFloorId);
                        DownSet.Add(sourceFloorId);
                    }
                    if (!UpSet.Contains(destinationFloorId))
                    {
                        Up.Enqueue(destinationFloorId, destinationFloorId);
                        UpSet.Add(destinationFloorId);
                    }
                }
                else
                {
                    if (!UpSet.Contains(sourceFloorId))
                    {
                        Up.Enqueue(sourceFloorId, sourceFloorId);
                        UpSet.Add(sourceFloorId);
                    }
                    if (!UpSet.Contains(destinationFloorId))
                    {
                        Up.Enqueue(destinationFloorId, destinationFloorId);
                        UpSet.Add(destinationFloorId);
                    }
                }
            }
            else if (direction == Direction.DOWN && (Status == StatusEnum.MOVING_UP))
            {
                if (sourceFloorId > _currentFloor)
                {
                    if (!UpSet.Contains(sourceFloorId))
                    {
                        Up.Enqueue(sourceFloorId, sourceFloorId);
                        UpSet.Add(sourceFloorId);
                    }
                    if (!DownSet.Contains(destinationFloorId))
                    {
                        Down.Enqueue(destinationFloorId, -destinationFloorId);
                        DownSet.Add(destinationFloorId);

                    }
                }
                else
                {
                    if (!DownSet.Contains(sourceFloorId))
                    {
                        Down.Enqueue(sourceFloorId, -sourceFloorId);
                        DownSet.Add(sourceFloorId);
                    }
                    if (!DownSet.Contains(destinationFloorId))
                    {
                        Down.Enqueue(destinationFloorId, -destinationFloorId);
                        DownSet.Add(destinationFloorId);
                    }
                }
            }

            if (Status == StatusEnum.IDLE)
            {
                Status = Up.Count > 0 ? StatusEnum.MOVING_UP : (Down.Count > 0 ? StatusEnum.MOVING_DOWN : StatusEnum.IDLE);

                Task.Run(() => Move());
            }
                //Move();
        }


        public void  Move()
        {
            if(Up.Count == 0 && Down.Count == 0)
            {
                Status = StatusEnum.IDLE;
                return;
            }
            if (Up.Count > 0)
            {
                Status = StatusEnum.MOVING_UP;
                while (Up.Count > 0)
                {
                    _currentFloor = Up.Dequeue();
                    UpSet.Remove(_currentFloor);
                    Console.WriteLine($"Elevator - {Id} is at floor {_currentFloor}");
                    Task.Delay(3000).Wait();//time taken to go to eahc floor
                }
                
                    foreach(int floorToGo in YetToStop)
                    {
                    if (!DownSet.Contains(floorToGo)) Down.Enqueue(floorToGo, -floorToGo);
                    DownSet.Add(floorToGo);
                       
                    }
                YetToStop.Clear();
            }
            if (Down.Count > 0)
            {
                Status = StatusEnum.MOVING_DOWN;
                while (Down.Count > 0)
                {
                    _currentFloor = Down.Dequeue();
                    DownSet.Remove(_currentFloor);
                    Console.WriteLine($"Elevator - {Id} is at floor {_currentFloor}");
                    Task.Delay(3000).Wait();//time taken to go to eahc floor
                }
                    foreach (int floorToGo in YetToStop)
                    {

                    if (!UpSet.Contains(floorToGo))
                        Up.Enqueue(floorToGo, floorToGo);
                    UpSet.Add(floorToGo);
                    }
                YetToStop.Clear();
            }
            Move();

        }
         





    }
}
