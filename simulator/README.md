# Toy Robot Simulator

A console application that simulates a toy robot moving on a 6x6 square tabletop.

## Description

This application allows for a simulation of a toy robot moving on a 6x6 square tabletop. The robot can be placed on the table, moved forward, rotated left or right, and report its current position. The robot is prevented from falling off the table - any movement that would result in this is ignored.

### Commands

| Command | Description |
|---------|-------------|
| `PLACE X,Y,DIRECTION` | Place the robot on the table at position (X,Y) facing NORTH, SOUTH, EAST, or WEST. |
| `MOVE` | Move the robot one unit forward in the direction it is currently facing. |
| `LEFT` | Rotate the robot 90 degrees to the left without changing its position. |
| `RIGHT` | Rotate the robot 90 degrees to the right without changing its position. |
| `REPORT` | Output the current position and direction of the robot (e.g., `0,1,NORTH`). |

### Rules

- The table is 6x6 with (0,0) as the SOUTH WEST corner and (5,5) as the NORTH EAST corner.
- The first valid command must be a `PLACE` command. All commands before a valid `PLACE` are ignored.
- The `PLACE` command is ignored if it would place the robot outside the table.
- Once placed, subsequent `PLACE` commands can omit the direction to keep the current facing.
- Movement commands that would cause the robot to fall off the table are ignored.
- Invalid commands and parameters are discarded.

## Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later

## Building the Project

Navigate to the project directory:

```bash
cd simulator
```

Build the solution:

```bash
dotnet build
```

## Running the Application

Run the simulator from the project root:

```bash
dotnet run --project src/Simulator
```

Or navigate to the project directory and run:

```bash
cd src/Simulator
dotnet run
```

### Interactive Usage

Once the application starts, you can enter commands interactively:

```
Toy Robot Simulator
==================

Commands:
- PLACE X,Y,DIRECTION
  ...

PLACE 0,0,NORTH
MOVE
REPORT
0,1,NORTH
```

Press `Ctrl+C` to exit the application.

### Example Session

```
PLACE 1,2,EAST
MOVE
MOVE
LEFT
MOVE
REPORT
3,3,NORTH
```

## Running Tests

Run all tests from the project root:

```bash
dotnet test
```

Run tests with detailed output:

```bash
dotnet test --verbosity normal
```


## Project Structure

```
simulator/
├── src/
│   └── Simulator/
│       ├── Program.cs         # Application entry point
│       ├── RobotSimulator.cs  # Main simulator loop
│       ├── Instructions/      # Command parsing and instruction types
│       │   ├── IInstruction.cs
│       │   ├── InputParser.cs
│       │   ├── PlaceCommand.cs
│       │   ├── MoveCommand.cs
│       │   ├── LeftCommand.cs
│       │   ├── RightCommand.cs
│       │   └── ReportQuery.cs
│       └── Robots/            # Robot and table domain models
│           ├── Direction.cs
│           ├── Robot.cs
│           └── Table.cs
├── tests/
│   └── Simulator.Tests/       # Unit and integration tests
│       ├── Instructions/
│       └── Robots/
├── Simulator.slnx             # Solution file
└── README.md
```
