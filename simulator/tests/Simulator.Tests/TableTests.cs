namespace Simulator.Tests;

public class TableTests
{
    [Theory]
    [InlineData(0, 0, true)]
    [InlineData(5, 5, true)]
    [InlineData(0, 5, true)]
    [InlineData(5, 0, true)]
    [InlineData(2, 3, true)]
    public void IsValidPosition_ValidPositions_ReturnsTrue(int x, int y, bool expected)
    {
        // Arrange
        var table = new Table();
        var position = new Position(x, y);

        // Act
        var result = table.IsValidPosition(position);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(6, 0)]
    [InlineData(0, 6)]
    [InlineData(-1, -1)]
    [InlineData(6, 6)]
    public void IsValidPosition_InvalidPositions_ReturnsFalse(int x, int y)
    {
        // Arrange
        var table = new Table();
        var position = new Position(x, y);

        // Act
        var result = table.IsValidPosition(position);

        // Assert
        Assert.False(result);
    }
}
