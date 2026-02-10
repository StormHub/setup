namespace Simulator.Tests;

public class RobotTests
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
        var position = new Position(1, 2);

        // Act
        var result = robot.Place(position, Direction.North);

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
        var position = new Position(x, y);

        // Act
        var result = robot.Place(position, Direction.North);

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
        var position = new Position(1, 1);

        // Act
        var result = robot.Place(position);

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
        robot.Place(new Position(0, 0), Direction.North);

        // Act
        var result = robot.Place(new Position(3, 3));

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
        var result = robot.Move();

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(0, 0, Direction.North, "0,1,NORTH")]
    [InlineData(0, 0, Direction.East, "1,0,EAST")]
    [InlineData(5, 5, Direction.South, "5,4,SOUTH")]
    [InlineData(5, 5, Direction.West, "4,5,WEST")]
    public void Move_InDirection_MovesRobotCorrectly(int x, int y, Direction direction, string expected)
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);
        robot.Place(new Position(x, y), direction);

        // Act
        var result = robot.Move();

        // Assert
        Assert.True(result);
        Assert.Equal(expected, robot.Report());
    }

    [Theory]
    [InlineData(0, 0, Direction.South)]
    [InlineData(0, 0, Direction.West)]
    [InlineData(5, 5, Direction.North)]
    [InlineData(5, 5, Direction.East)]
    public void Move_WouldFallOffTable_DoesNotMove(int x, int y, Direction direction)
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);
        robot.Place(new Position(x, y), direction);
        var originalReport = robot.Report();

        // Act
        var result = robot.Move();

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
        var result = robot.Left();

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(Direction.North, "0,0,WEST")]
    [InlineData(Direction.West, "0,0,SOUTH")]
    [InlineData(Direction.South, "0,0,EAST")]
    [InlineData(Direction.East, "0,0,NORTH")]
    public void Left_RotatesRobotCounterClockwise(Direction initialDirection, string expected)
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);
        robot.Place(new Position(0, 0), initialDirection);

        // Act
        var result = robot.Left();

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
        var result = robot.Right();

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(Direction.North, "0,0,EAST")]
    [InlineData(Direction.East, "0,0,SOUTH")]
    [InlineData(Direction.South, "0,0,WEST")]
    [InlineData(Direction.West, "0,0,NORTH")]
    public void Right_RotatesRobotClockwise(Direction initialDirection, string expected)
    {
        // Arrange
        var table = new Table();
        var robot = new Robot(table);
        robot.Place(new Position(0, 0), initialDirection);

        // Act
        var result = robot.Right();

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
        robot.Place(new Position(3, 4), Direction.South);

        // Act
        var result = robot.Report();

        // Assert
        Assert.Equal("3,4,SOUTH", result);
    }
}
