# Elevator System LLD

## Description
This project is a Low-Level Design (LLD) implementation of an Elevator System. It simulates the behavior of elevators in a building, including handling requests from different floors and moving the elevators accordingly.

## Features
- Create multiple elevators and floors.
- Handle requests to move between floors.
- Select elevators based on different strategies.
- Blacklist certain floors for specific elevators.

## Installation
1. Clone the repository:
2. Open the solution in Visual Studio 2022.
3. Build the solution to restore the dependencies and compile the project.

## Usage
1. Run the `Program.cs` file to start the simulation.
2. The program will create elevators and floors, and simulate user requests to move between floors.

## Algorithm in ElevatorCar
The `ElevatorCar` class uses a priority queue-based algorithm to manage the movement of the elevator. 