using Simulator.Robots;

namespace Simulator.Tests.Robots;

public sealed class RobotTests
{
    [Fact]
    public void Robot_InitialState_IsNotPlaced()
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);

        // Act & Assert
        Assert.False(robot.IsPlaced);
        Assert.Equal(string.Empty, robot.Report());
    }

    [Fact]
    public void Place_WithValidPositionAndDirection_PlacesRobot()
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);

        // Act
        var result = robot.TryPlace(1, 2, Direction.North);

        // Assert
        Assert.True(result);
        Assert.True(robot.IsPlaced);
        Assert.Equal("1,2,NORTH", robot.Report());
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(6, 0)]
    [InlineData(0, 6)]
    public void Place_WithInvalidPosition_DoesNotPlaceRobot(int x, int y)
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);

        // Act
        var result = robot.TryPlace(x, y, Direction.North);

        // Assert
        Assert.False(result);
        Assert.False(robot.IsPlaced);
    }

    [Fact]
    public void Place_WithPositionOnly_WhenNotPlaced_DoesNotPlaceRobot()
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);

        // Act
        var result = robot.TryPlace(1, 1);

        // Assert
        Assert.False(result);
        Assert.False(robot.IsPlaced);
    }

    [Fact]
    public void Place_WithPositionOnly_WhenPlaced_MovesRobotWithoutChangingDirection()
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);
        robot.TryPlace(0, 0, Direction.North);

        // Act
        var result = robot.TryPlace(3, 3);

        // Assert
        Assert.True(result);
        Assert.Equal("3,3,NORTH", robot.Report());
    }

    [Fact]
    public void Move_WhenNotPlaced_ReturnsFalse()
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);

        // Act
        var result = robot.TryMove();

        // Assert
        Assert.False(result);
    }

    public static TheoryData<int, int, Direction, string> MoveInDirectionData =>
        new()
        {
            { 0, 0, Direction.North, "0,1,NORTH" },
            { 0, 0, Direction.East, "1,0,EAST" },
            { 5, 5, Direction.South, "5,4,SOUTH" },
            { 5, 5, Direction.West, "4,5,WEST" }
        };

    [Theory]
    [MemberData(nameof(MoveInDirectionData))]
    public void Move_InDirection_MovesRobotCorrectly(int x, int y, Direction direction, string expected)
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);
        robot.TryPlace(x, y, direction);

        // Act
        var result = robot.TryMove();

        // Assert
        Assert.True(result);
        Assert.Equal(expected, robot.Report());
    }

    public static TheoryData<int, int, Direction> MoveWouldFallOffTableData =>
        new()
        {
            { 0, 0, Direction.South },
            { 0, 0, Direction.West },
            { 5, 5, Direction.North },
            { 5, 5, Direction.East }
        };

    [Theory]
    [MemberData(nameof(MoveWouldFallOffTableData))]
    public void Move_WouldFallOffTable_DoesNotMove(int x, int y, Direction direction)
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);
        robot.TryPlace(x, y, direction);
        var originalReport = robot.Report();

        // Act
        var result = robot.TryMove();

        // Assert
        Assert.False(result);
        Assert.Equal(originalReport, robot.Report());
    }

    [Fact]
    public void Left_WhenNotPlaced_ReturnsFalse()
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);

        // Act
        var result = robot.TryTurnLeft();

        // Assert
        Assert.False(result);
    }

    public static TheoryData<Direction, string> LeftRotatesCounterClockwiseData =>
        new()
        {
            { Direction.North, "0,0,WEST" },
            { Direction.West, "0,0,SOUTH" },
            { Direction.South, "0,0,EAST" },
            { Direction.East, "0,0,NORTH" }
        };

    [Theory]
    [MemberData(nameof(LeftRotatesCounterClockwiseData))]
    public void Left_RotatesRobotCounterClockwise(Direction initialDirection, string expected)
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);
        robot.TryPlace(0, 0, initialDirection);

        // Act
        var result = robot.TryTurnLeft();

        // Assert
        Assert.True(result);
        Assert.Equal(expected, robot.Report());
    }

    [Fact]
    public void Right_WhenNotPlaced_ReturnsFalse()
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);

        // Act
        var result = robot.TryTurnRight();

        // Assert
        Assert.False(result);
    }

    public static TheoryData<Direction, string> RightRotatesClockwiseData =>
        new()
        {
            { Direction.North, "0,0,EAST" },
            { Direction.East, "0,0,SOUTH" },
            { Direction.South, "0,0,WEST" },
            { Direction.West, "0,0,NORTH" }
        };

    [Theory]
    [MemberData(nameof(RightRotatesClockwiseData))]
    public void Right_RotatesRobotClockwise(Direction initialDirection, string expected)
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);
        robot.TryPlace(0, 0, initialDirection);

        // Act
        var result = robot.TryTurnRight();

        // Assert
        Assert.True(result);
        Assert.Equal(expected, robot.Report());
    }

    [Fact]
    public void Report_WhenNotPlaced_ReturnsEmptyString()
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);

        // Act
        var result = robot.Report();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void Report_WhenPlaced_ReturnsCorrectFormat()
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);
        robot.TryPlace(3, 4, Direction.South);

        // Act
        var result = robot.Report();

        // Assert
        Assert.Equal("3,4,SOUTH", result);
    }
}
