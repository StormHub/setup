using Simulator.Robots;

namespace Simulator.Tests.Robots;

public sealed class DirectionTests
{
    public static TheoryData<Direction, Direction> RotateLeftData =>
        new()
        {
            { Direction.North, Direction.West },
            { Direction.West, Direction.South },
            { Direction.South, Direction.East },
            { Direction.East, Direction.North }
        };

    [Theory]
    [MemberData(nameof(RotateLeftData))]
    public void RotateLeft_ReturnsCorrectDirection(Direction input, Direction expected)
    {
        // Act
        var result = input.RotateLeft();

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<Direction, Direction> RotateRightData =>
        new()
        {
            { Direction.North, Direction.East },
            { Direction.East, Direction.South },
            { Direction.South, Direction.West },
            { Direction.West, Direction.North }
        };

    [Theory]
    [MemberData(nameof(RotateRightData))]
    public void RotateRight_ReturnsCorrectDirection(Direction input, Direction expected)
    {
        // Act
        var result = input.RotateRight();

        // Assert
        Assert.Equal(expected, result);
    }

    public static TheoryData<Direction, int, int> MovementDeltaData =>
        new()
        {
            { Direction.North, 0, 1 },
            { Direction.East, 1, 0 },
            { Direction.South, 0, -1 },
            { Direction.West, -1, 0 }
        };

    [Theory]
    [MemberData(nameof(MovementDeltaData))]
    public void GetMovementDelta_ReturnsCorrectDelta(Direction direction, int expectedX, int expectedY)
    {
        // Act
        var (deltaX, deltaY) = direction.GetMovementDelta();

        // Assert
        Assert.Equal(expectedX, deltaX);
        Assert.Equal(expectedY, deltaY);
    }

    public static TheoryData<string, Direction> TryParseValidData =>
        new()
        {
            { "NORTH", Direction.North },
            { "north", Direction.North },
            { "North", Direction.North },
            { "EAST", Direction.East },
            { "SOUTH", Direction.South },
            { "WEST", Direction.West }
        };

    [Theory]
    [MemberData(nameof(TryParseValidData))]
    public void TryParse_ValidInput_ReturnsTrue(string input, Direction expected)
    {
        // Act
        var result = Direction.TryParse(input, out var direction);

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
        var result = Direction.TryParse(input, out var _);

        // Assert
        Assert.False(result);
    }
}
