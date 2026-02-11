using Microsoft.Extensions.Logging.Abstractions;
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
            new PlaceCommand(0, 0, Direction.North, NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance));

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
            new PlaceCommand(0, 0, Direction.North, NullLoggerFactory.Instance),
            new LeftCommand(NullLoggerFactory.Instance));

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
            new PlaceCommand(1, 2, Direction.East, NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new LeftCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance));

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
            new PlaceCommand(1, 2, Direction.East, NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new LeftCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new PlaceCommand(3, 1, null, NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance));

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
            new MoveCommand(NullLoggerFactory.Instance),
            new LeftCommand(NullLoggerFactory.Instance),
            new RightCommand(NullLoggerFactory.Instance),
            new PlaceCommand(1, 1, Direction.North, NullLoggerFactory.Instance));

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
            new PlaceCommand(6, 6, Direction.North, NullLoggerFactory.Instance),
            new PlaceCommand(1, 1, Direction.North, NullLoggerFactory.Instance));

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
            new PlaceCommand(0, 0, Direction.South, NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance));

        // Assert
        Assert.Equal("0,0,SOUTH", simulator.Report());
    }

    [Fact]
    public void MultipleReports_OutputMultipleTimes()
    {
        // Arrange
        var simulator = new RobotSimulator();

        // Act
        simulator.Execute(new PlaceCommand(0, 0, Direction.North, NullLoggerFactory.Instance));
        var output1 = simulator.Report();
        
        simulator.Execute(new MoveCommand(NullLoggerFactory.Instance));
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
        simulator.Execute(new PlaceCommand(x, y, Direction.North, NullLoggerFactory.Instance));

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
            new PlaceCommand(0, 5, Direction.East, NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance));

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
            new PlaceCommand(2, 2, Direction.North, NullLoggerFactory.Instance),
            new RightCommand(NullLoggerFactory.Instance),
            new RightCommand(NullLoggerFactory.Instance),
            new RightCommand(NullLoggerFactory.Instance),
            new RightCommand(NullLoggerFactory.Instance));

        // Assert
        Assert.Equal("2,2,NORTH", simulator.Report());
    }
}
