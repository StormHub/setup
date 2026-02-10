using Simulator.Robots;

namespace Simulator.Tests.Robots;

public sealed class DirectionExtensionsTests
{
    [Theory]
    [InlineData(Direction.North, Direction.West)]
    [InlineData(Direction.West, Direction.South)]
    [InlineData(Direction.South, Direction.East)]
    [InlineData(Direction.East, Direction.North)]
    public void RotateLeft_ReturnsCorrectDirection(Direction input, Direction expected)
    {
        // Act
        var result = input.RotateLeft();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(Direction.North, Direction.East)]
    [InlineData(Direction.East, Direction.South)]
    [InlineData(Direction.South, Direction.West)]
    [InlineData(Direction.West, Direction.North)]
    public void RotateRight_ReturnsCorrectDirection(Direction input, Direction expected)
    {
        // Act
        var result = input.RotateRight();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(Direction.North, 0, 1)]
    [InlineData(Direction.East, 1, 0)]
    [InlineData(Direction.South, 0, -1)]
    [InlineData(Direction.West, -1, 0)]
    public void GetMovementDelta_ReturnsCorrectDelta(Direction direction, int expectedX, int expectedY)
    {
        // Act
        var (deltaX, deltaY) = direction.GetMovementDelta();

        // Assert
        Assert.Equal(expectedX, deltaX);
        Assert.Equal(expectedY, deltaY);
    }

    [Theory]
    [InlineData("NORTH", Direction.North)]
    [InlineData("north", Direction.North)]
    [InlineData("North", Direction.North)]
    [InlineData("EAST", Direction.East)]
    [InlineData("SOUTH", Direction.South)]
    [InlineData("WEST", Direction.West)]
    public void TryParse_ValidInput_ReturnsTrue(string input, Direction expected)
    {
        // Act
        var result = DirectionExtensions.TryParse(input, out var direction);

        // Assert
        Assert.True(result);
        Assert.Equal(expected, direction);
    }

    [Theory]
    [InlineData("INVALID")]
    [InlineData("")]
    [InlineData("NE")]
    public void TryParse_InvalidInput_ReturnsFalse(string input)
    {
        // Act
        var result = DirectionExtensions.TryParse(input, out var _);

        // Assert
        Assert.False(result);
    }
}
