using Simulator.Instructions;
using Simulator.Robots;

namespace Simulator.Tests.Robots;

public sealed class RobotSimulatorTests
{
    [Fact]
    public void ScenarioA_PlaceMoveReport_OutputsCorrectPosition()
    {
        // Arrange
        var simulator = new RobotSimulator();

        // Act
        simulator.Execute(
            new PlaceCommand(0, 0, Direction.North),
            new MoveCommand());

        // Assert
        Assert.Equal("0,1,NORTH", simulator.Report());
    }

    [Fact]
    public void ScenarioB_PlaceLeftReport_OutputsCorrectPosition()
    {
        // Arrange
        var simulator = new RobotSimulator();

        // Act
        simulator.Execute(
            new PlaceCommand(0, 0, Direction.North),
            new LeftCommand());

        // Assert
        Assert.Equal("0,0,WEST", simulator.Report());
    }

    [Fact]
    public void ScenarioC_ComplexMovementSequence_OutputsCorrectPosition()
    {
        // Arrange
        var simulator = new RobotSimulator();

        // Act
        simulator.Execute(
            new PlaceCommand(1, 2, Direction.East),
            new MoveCommand(),
            new MoveCommand(),
            new LeftCommand(),
            new MoveCommand());

        // Assert
        Assert.Equal("3,3,NORTH", simulator.Report());
    }

    [Fact]
    public void ScenarioD_PlaceWithoutDirection_OutputsCorrectPosition()
    {
        // Arrange
        var simulator = new RobotSimulator();

        // Act
        simulator.Execute(
            new PlaceCommand(1, 2, Direction.East),
            new MoveCommand(),
            new LeftCommand(),
            new MoveCommand(),
            new PlaceCommand(3, 1, null),
            new MoveCommand());

        // Assert
        Assert.Equal("3,2,NORTH", simulator.Report());
    }

    [Fact]
    public void CommandsBeforeFirstPlace_AreIgnored()
    {
        // Arrange
        var simulator = new RobotSimulator();

        // Act
        simulator.Execute(
            new MoveCommand(),
            new LeftCommand(),
            new RightCommand(),
            new PlaceCommand(1, 1, Direction.North));

        // Assert
        Assert.Equal("1,1,NORTH", simulator.Report());
    }

    [Fact]
    public void InvalidPlaceCommand_DoesNotPlaceRobot()
    {
        // Arrange
        var simulator = new RobotSimulator();

        // Act
        simulator.Execute(
            new PlaceCommand(6, 6, Direction.North),
            new PlaceCommand(1, 1, Direction.North));

        // Assert
        Assert.Equal("1,1,NORTH", simulator.Report());
    }

    [Fact]
    public void MovePreventingFall_DoesNotMoveRobot()
    {
        // Arrange
        var simulator = new RobotSimulator();

        // Act
        simulator.Execute(
            new PlaceCommand(0, 0, Direction.South),
            new MoveCommand());

        // Assert
        Assert.Equal("0,0,SOUTH", simulator.Report());
    }

    [Fact]
    public void MultipleReports_OutputMultipleTimes()
    {
        // Arrange
        var simulator = new RobotSimulator();

        // Act
        simulator.Execute(new PlaceCommand(0, 0, Direction.North));
        var output1 = simulator.Report();
        
        simulator.Execute(new MoveCommand());
        var output2 = simulator.Report();

        // Assert
        Assert.Equal("0,0,NORTH", output1);
        Assert.Equal("0,1,NORTH", output2);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(5, 5)]
    [InlineData(0, 5)]
    [InlineData(5, 0)]
    public void PlaceAtCorners_WorksCorrectly(int x, int y)
    {
        // Arrange
        var simulator = new RobotSimulator();

        // Act
        simulator.Execute(new PlaceCommand(x, y, Direction.North));

        // Assert
        Assert.Equal($"{x},{y},NORTH", simulator.Report());
    }

    [Fact]
    public void RobotMovingAlongEdges_DoesNotFall()
    {
        // Arrange
        var simulator = new RobotSimulator();

        // Act - Move along north edge
        simulator.Execute(
            new PlaceCommand(0, 5, Direction.East),
            new MoveCommand(),
            new MoveCommand(),
            new MoveCommand(),
            new MoveCommand(),
            new MoveCommand());

        // Assert
        Assert.Equal("5,5,EAST", simulator.Report());
    }

    [Fact]
    public void RotationsOnly_DoNotChangePosition()
    {
        // Arrange
        var simulator = new RobotSimulator();

        // Act - Four right turns = full rotation
        simulator.Execute(
            new PlaceCommand(2, 2, Direction.North),
            new RightCommand(),
            new RightCommand(),
            new RightCommand(),
            new RightCommand());

        // Assert
        Assert.Equal("2,2,NORTH", simulator.Report());
    }
}
