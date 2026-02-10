using Simulator.Robots;

namespace Simulator.Tests.Robots;

public sealed class TableTests
{
    [Fact]
    public void Size_DefaultsToSix()
    {
        // Arrange & Act
        var table = new Table();

        // Assert
        Assert.Equal(6, table.Size);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(5, 5)]
    [InlineData(0, 5)]
    [InlineData(5, 0)]
    [InlineData(2, 3)]
    public void TryPlace_ValidPositions_ReturnsTrueAndPosition(int x, int y)
    {
        // Arrange
        var table = new Table();

        // Act
        var result = table.TryPlace(x, y, out var position);

        // Assert
        Assert.True(result);
        Assert.NotNull(position);
        Assert.Equal(x, position.X);
        Assert.Equal(y, position.Y);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(6, 0)]
    [InlineData(0, 6)]
    [InlineData(-1, -1)]
    [InlineData(6, 6)]
    public void TryPlace_InvalidPositions_ReturnsFalseAndNull(int x, int y)
    {
        // Arrange
        var table = new Table();

        // Act
        var result = table.TryPlace(x, y, out var position);

        // Assert
        Assert.False(result);
        Assert.Null(position);
    }
}
