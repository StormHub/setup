using Simulator.Instructions;
using Simulator.Robots;

namespace Simulator.Tests.Instructions;

public sealed class InputParserTests
{
    [Fact]
    public void TryParse_PlaceWithDirectionCommand_ReturnsPlaceCommand()
    {
        // Arrange
        using var output = new StringWriter();
        var parser = new InputParser(output, null);

        // Act
        var result = parser.TryParse("PLACE 1,2,NORTH", out var instruction);

        // Assert
        Assert.True(result);
        Assert.NotNull(instruction);
        Assert.IsType<PlaceCommand>(instruction);
        
        // Verify the place command works correctly by executing it
        var robot = new Robot(new Table());
        instruction.Execute(robot);
        Assert.Equal("1,2,NORTH", robot.Report());
    }

    [Fact]
    public void TryParse_PlaceWithoutDirectionCommand_ReturnsPlaceCommand()
    {
        // Arrange
        using var output = new StringWriter();
        var parser = new InputParser(output, null);

        // Act
        var result = parser.TryParse("PLACE 3,4", out var instruction);

        // Assert
        Assert.True(result);
        Assert.NotNull(instruction);
        var placeCommand = Assert.IsType<PlaceCommand>(instruction);
        Assert.Equal(3, placeCommand.X);
        Assert.Equal(4, placeCommand.Y);
        Assert.Null(placeCommand.Direction);
    }

    [Theory]
    [InlineData("PLACE 1,2,NORTH")]
    [InlineData("place 1,2,north")]
    [InlineData("Place 1,2,North")]
    public void TryParse_PlaceCommand_IsCaseInsensitive(string input)
    {
        // Arrange
        using var output = new StringWriter();
        var parser = new InputParser(output, null);

        // Act
        var result = parser.TryParse(input, out var instruction);

        // Assert
        Assert.True(result);
        Assert.NotNull(instruction);
        Assert.IsType<PlaceCommand>(instruction);
    }

    [Fact]
    public void TryParse_MoveCommand_ReturnsMoveCommand()
    {
        // Arrange
        using var output = new StringWriter();
        var parser = new InputParser(output, null);

        // Act
        var result = parser.TryParse("MOVE", out var instruction);

        // Assert
        Assert.True(result);
        Assert.NotNull(instruction);
        Assert.IsType<MoveCommand>(instruction);
    }

    [Fact]
    public void TryParse_LeftCommand_ReturnsLeftCommand()
    {
        // Arrange
        using var output = new StringWriter();
        var parser = new InputParser(output, null);

        // Act
        var result = parser.TryParse("LEFT", out var instruction);

        // Assert
        Assert.True(result);
        Assert.NotNull(instruction);
        Assert.IsType<LeftCommand>(instruction);
    }

    [Fact]
    public void TryParse_RightCommand_ReturnsRightCommand()
    {
        // Arrange
        using var output = new StringWriter();
        var parser = new InputParser(output, null);

        // Act
        var result = parser.TryParse("RIGHT", out var instruction);

        // Assert
        Assert.True(result);
        Assert.NotNull(instruction);
        Assert.IsType<RightCommand>(instruction);
    }

    [Fact]
    public void TryParse_ReportQuery_ReturnsReportQuery()
    {
        // Arrange
        using var output = new StringWriter();
        var parser = new InputParser(output, null);

        // Act
        var result = parser.TryParse("REPORT", out var instruction);

        // Assert
        Assert.True(result);
        Assert.NotNull(instruction);
        var reportQuery = Assert.IsType<ReportQuery>(instruction);
        
        // Verify the output is written correctly when executed
        var robot = new Robot(new Table());
        robot.TryPlace(1, 2, Direction.North);
        reportQuery.Execute(robot);
        Assert.Equal("1,2,NORTH", output.ToString().TrimEnd());
    }

    [Theory]
    [InlineData("REPORT")]
    [InlineData("report")]
    [InlineData("Report")]
    public void TryParse_ReportQuery_IsCaseInsensitive(string input)
    {
        // Arrange
        using var output = new StringWriter();
        var parser = new InputParser(output, null);

        // Act
        var result = parser.TryParse(input, out var instruction);

        // Assert
        Assert.True(result);
        Assert.NotNull(instruction);
        Assert.IsType<ReportQuery>(instruction);
    }

    [Theory]
    [InlineData("INVALID")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("PLACE")]
    [InlineData("PLACE 1")]
    [InlineData("PLACE 1,2,3,4")]
    [InlineData("PLACE a,b,NORTH")]
    [InlineData("PLACE 1,2,INVALID")]
    public void TryParse_InvalidCommand_ReturnsFalseAndNull(string input)
    {
        // Arrange
        using var output = new StringWriter();
        var parser = new InputParser(output, null);

        // Act
        var result = parser.TryParse(input, out var instruction);

        // Assert
        Assert.False(result);
        Assert.Null(instruction);
    }

    [Theory]
    [InlineData("  PLACE 1,2,NORTH  ")]
    [InlineData("PLACE  1,2,NORTH")]
    [InlineData("  MOVE  ")]
    [InlineData("  REPORT  ")]
    [InlineData("PLACE 1, 2, NORTH")]
    [InlineData("PLACE 1 , 2 , NORTH")]
    [InlineData("PLACE   1,  2,  NORTH")]
    public void TryParse_CommandWithExtraWhitespace_ParsesCorrectly(string input)
    {
        // Arrange
        using var output = new StringWriter();
        var parser = new InputParser(output, null);

        // Act
        var result = parser.TryParse(input, out var instruction);

        // Assert
        Assert.True(result);
        Assert.NotNull(instruction);
    }
}
