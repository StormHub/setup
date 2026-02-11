using Microsoft.Extensions.Logging.Abstractions;
using Simulator.Instructions;
using Simulator.Robots;

namespace Simulator.Tests.Robots;

public sealed class RobotSimulatorTests
{
    private readonly RobotSimulator _simulator = new(new InputParser(TextWriter.Null, null));

    [Fact]
    public void ScenarioA_PlaceMoveReport_OutputsCorrectPosition()
    {
        // Act
        _simulator.Execute(
            new PlaceCommand(0, 0, Direction.North, NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance));

        // Assert
        Assert.Equal("0,1,NORTH", _simulator.Report());
    }

    [Fact]
    public void ScenarioB_PlaceLeftReport_OutputsCorrectPosition()
    {
        // Act
        _simulator.Execute(
            new PlaceCommand(0, 0, Direction.North, NullLoggerFactory.Instance),
            new LeftCommand(NullLoggerFactory.Instance));

        // Assert
        Assert.Equal("0,0,WEST", _simulator.Report());
    }

    [Fact]
    public void ScenarioC_ComplexMovementSequence_OutputsCorrectPosition()
    {
        // Act
        _simulator.Execute(
            new PlaceCommand(1, 2, Direction.East, NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new LeftCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance));

        // Assert
        Assert.Equal("3,3,NORTH", _simulator.Report());
    }

    [Fact]
    public void ScenarioD_PlaceWithoutDirection_OutputsCorrectPosition()
    {
        // Act
        _simulator.Execute(
            new PlaceCommand(1, 2, Direction.East, NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new LeftCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new PlaceCommand(3, 1, null, NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance));

        // Assert
        Assert.Equal("3,2,NORTH", _simulator.Report());
    }

    [Fact]
    public void CommandsBeforeFirstPlace_AreIgnored()
    {
        // Act
        _simulator.Execute(
            new MoveCommand(NullLoggerFactory.Instance),
            new LeftCommand(NullLoggerFactory.Instance),
            new RightCommand(NullLoggerFactory.Instance),
            new PlaceCommand(1, 1, Direction.North, NullLoggerFactory.Instance));

        // Assert
        Assert.Equal("1,1,NORTH", _simulator.Report());
    }

    [Fact]
    public void InvalidPlaceCommand_DoesNotPlaceRobot()
    {
        // Act
        _simulator.Execute(
            new PlaceCommand(6, 6, Direction.North, NullLoggerFactory.Instance),
            new PlaceCommand(1, 1, Direction.North, NullLoggerFactory.Instance));

        // Assert
        Assert.Equal("1,1,NORTH", _simulator.Report());
    }

    [Fact]
    public void MovePreventingFall_DoesNotMoveRobot()
    {
        // Act
        _simulator.Execute(
            new PlaceCommand(0, 0, Direction.South, NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance));

        // Assert
        Assert.Equal("0,0,SOUTH", _simulator.Report());
    }

    [Fact]
    public void MultipleReports_OutputMultipleTimes()
    {
        // Act
        _simulator.Execute(new PlaceCommand(0, 0, Direction.North, NullLoggerFactory.Instance));
        var output1 = _simulator.Report();
        
        _simulator.Execute(new MoveCommand(NullLoggerFactory.Instance));
        var output2 = _simulator.Report();

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
        // Act
        _simulator.Execute(new PlaceCommand(x, y, Direction.North, NullLoggerFactory.Instance));

        // Assert
        Assert.Equal($"{x},{y},NORTH", _simulator.Report());
    }

    [Fact]
    public void RobotMovingAlongEdges_DoesNotFall()
    {
        // Act - Move along north edge
        _simulator.Execute(
            new PlaceCommand(0, 5, Direction.East, NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance),
            new MoveCommand(NullLoggerFactory.Instance));

        // Assert
        Assert.Equal("5,5,EAST", _simulator.Report());
    }

    [Fact]
    public void RotationsOnly_DoNotChangePosition()
    {
        // Act - Four right turns = full rotation
        _simulator.Execute(
            new PlaceCommand(2, 2, Direction.North, NullLoggerFactory.Instance),
            new RightCommand(NullLoggerFactory.Instance),
            new RightCommand(NullLoggerFactory.Instance),
            new RightCommand(NullLoggerFactory.Instance),
            new RightCommand(NullLoggerFactory.Instance));

        // Assert
        Assert.Equal("2,2,NORTH", _simulator.Report());
    }
}
