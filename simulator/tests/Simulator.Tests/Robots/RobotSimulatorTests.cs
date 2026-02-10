using Simulator.Instructions;
using Simulator.Instructions.Commands;
using Simulator.Robots;

namespace Simulator.Tests.Robots;

public sealed class RobotSimulatorTests
{
    [Fact]
    public void ScenarioA_PlaceMoveReport_OutputsCorrectPosition()
    {
        // Arrange
        var simulator = new RobotSimulator();
        var parser = new InputParser();

        // Act
        var commands = new[] { "PLACE 0,0,NORTH", "MOVE" }
            .Select(c => parser.Parse(c))
            .Where(c => c != null)
            .Cast<ICommand>();
        simulator.Execute(commands);

        // Assert
        Assert.Equal("0,1,NORTH", simulator.Report());
    }

    [Fact]
    public void ScenarioB_PlaceLeftReport_OutputsCorrectPosition()
    {
        // Arrange
        var simulator = new RobotSimulator();
        var parser = new InputParser();

        // Act
        var commands = new[] { "PLACE 0,0,NORTH", "LEFT" }
            .Select(c => parser.Parse(c))
            .Where(c => c != null)
            .Cast<ICommand>();
        simulator.Execute(commands);

        // Assert
        Assert.Equal("0,0,WEST", simulator.Report());
    }

    [Fact]
    public void ScenarioC_ComplexMovementSequence_OutputsCorrectPosition()
    {
        // Arrange
        var simulator = new RobotSimulator();
        var parser = new InputParser();

        // Act
        var commands = new[] { "PLACE 1,2,EAST", "MOVE", "MOVE", "LEFT", "MOVE" }
            .Select(c => parser.Parse(c))
            .Where(c => c != null)
            .Cast<ICommand>();
        simulator.Execute(commands);

        // Assert
        Assert.Equal("3,3,NORTH", simulator.Report());
    }

    [Fact]
    public void ScenarioD_PlaceWithoutDirection_OutputsCorrectPosition()
    {
        // Arrange
        var simulator = new RobotSimulator();
        var parser = new InputParser();

        // Act
        var commands = new[] { "PLACE 1,2,EAST", "MOVE", "LEFT", "MOVE", "PLACE 3,1", "MOVE" }
            .Select(c => parser.Parse(c))
            .Where(c => c != null)
            .Cast<ICommand>();
        simulator.Execute(commands);
        simulator.Report();

        // Assert
        Assert.Equal("3,2,NORTH", simulator.Report());
    }

    [Fact]
    public void CommandsBeforeFirstPlace_AreIgnored()
    {
        // Arrange
        var simulator = new RobotSimulator();
        var parser = new InputParser();

        // Act
        var commands = new[] { "MOVE", "LEFT", "RIGHT", "PLACE 1,1,NORTH" }
            .Select(c => parser.Parse(c))
            .Where(c => c != null)
            .Cast<ICommand>();
        simulator.Execute(commands);

        // Assert
        Assert.Equal("1,1,NORTH", simulator.Report());
    }

    [Fact]
    public void InvalidPlaceCommand_DoesNotPlaceRobot()
    {
        // Arrange
        var simulator = new RobotSimulator();
        var parser = new InputParser();

        // Act
        var commands = new[] { "PLACE 6,6,NORTH", "PLACE 1,1,NORTH" }
            .Select(c => parser.Parse(c))
            .Where(c => c != null)
            .Cast<ICommand>();
        simulator.Execute(commands);
        simulator.Report();

        // Assert
        Assert.Equal("1,1,NORTH", simulator.Report());
    }

    [Fact]
    public void MovePreventingFall_DoesNotMoveRobot()
    {
        // Arrange
        var simulator = new RobotSimulator();
        var parser = new InputParser();

        // Act
        var commands = new[] { "PLACE 0,0,SOUTH", "MOVE" }
            .Select(c => parser.Parse(c))
            .Where(c => c != null)
            .Cast<ICommand>();
        simulator.Execute(commands);

        // Assert
        Assert.Equal("0,0,SOUTH", simulator.Report());
    }

    [Fact]
    public void InvalidCommands_AreIgnored()
    {
        // Arrange
        var simulator = new RobotSimulator();
        var parser = new InputParser();

        // Act
        var commands = new[] { "PLACE 1,1,NORTH", "INVALID", "JUMP" }
            .Select(c => parser.Parse(c))
            .Where(c => c != null)
            .Cast<ICommand>();
        simulator.Execute(commands);
        simulator.Report();

        // Assert
        Assert.Equal("1,1,NORTH", simulator.Report());
    }

    [Fact]
    public void MultipleReports_OutputMultipleTimes()
    {
        // Arrange
        var simulator = new RobotSimulator();
        var parser = new InputParser();

        // Act
        var commands = new[] { "PLACE 0,0,NORTH" }
            .Select(c => parser.Parse(c))
            .Where(c => c != null)
            .Cast<ICommand>();
        simulator.Execute(commands);
        var output1 = simulator.Report();
        
        var moveCommand = parser.Parse("MOVE");
        var parsedMoveCommand = Assert.IsType<MoveCommand>(moveCommand);
        simulator.Execute(parsedMoveCommand);
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
        var parser = new InputParser();

        // Act
        var placeCommand = parser.Parse($"PLACE {x},{y},NORTH");
        var parsedCommand = Assert.IsType<PlaceCommand>(placeCommand);
        simulator.Execute(parsedCommand);

        // Assert
        Assert.Equal($"{x},{y},NORTH", simulator.Report());
    }

    [Fact]
    public void RobotMovingAlongEdges_DoesNotFall()
    {
        // Arrange
        var simulator = new RobotSimulator();
        var parser = new InputParser();

        // Act - Move along north edge
        var commands = new[]
        {
            "PLACE 0,5,EAST",
            "MOVE",
            "MOVE",
            "MOVE",
            "MOVE",
            "MOVE"
        }
            .Select(c => parser.Parse(c))
            .Where(c => c != null)
            .Cast<ICommand>();
        simulator.Execute(commands);

        // Assert
        Assert.Equal("5,5,EAST", simulator.Report());
    }

    [Fact]
    public void RotationsOnly_DoNotChangePosition()
    {
        // Arrange
        var simulator = new RobotSimulator();
        var parser = new InputParser();

        // Act - Four right turns = full rotation
        var commands = new[]
        {
            "PLACE 2,2,NORTH",
            "RIGHT",
            "RIGHT",
            "RIGHT",
            "RIGHT"
        }
            .Select(c => parser.Parse(c))
            .Where(c => c != null)
            .Cast<ICommand>();
        simulator.Execute(commands);
        simulator.Report();

        // Assert
        Assert.Equal("2,2,NORTH", simulator.Report());
    }
}
