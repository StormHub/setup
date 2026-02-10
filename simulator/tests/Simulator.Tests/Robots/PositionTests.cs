using Simulator.Robots;

namespace Simulator.Tests.Robots;

public sealed class PositionTests
{
    [Theory]
    [InlineData(0, 0, Direction.North, 0, 1)]
    [InlineData(0, 0, Direction.East, 1, 0)]
    [InlineData(1, 1, Direction.South, 1, 0)]
    [InlineData(1, 1, Direction.West, 0, 1)]
    public void Move_ReturnsNewPosition(int x, int y, Direction direction, int expectedX, int expectedY)
    {
        // Arrange
        var position = new Position(x, y);

        // Act
        var newPosition = position.Move(direction);

        // Assert
        Assert.Equal(expectedX, newPosition.X);
        Assert.Equal(expectedY, newPosition.Y);
    }

    [Fact]
    public void Move_DoesNotMutateOriginal()
    {
        // Arrange
        var position = new Position(1, 1);

        // Act
        var newPosition = position.Move(Direction.North);

        // Assert
        Assert.Equal(1, position.X);
        Assert.Equal(1, position.Y);
        Assert.Equal(1, newPosition.X);
        Assert.Equal(2, newPosition.Y);
    }
}
