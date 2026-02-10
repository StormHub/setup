using Simulator.Instructions;
using Simulator.Instructions.Commands;
using Simulator.Instructions.Queries;
using Simulator.Robots;

namespace Simulator.Tests.Instructions;

public sealed class InputParserTests
{
    [Fact]
    public void Parse_PlaceWithDirectionCommand_ReturnsPlaceCommand()
    {
        // Arrange
        var parser = new InputParser();

        // Act
        var instruction = parser.Parse("PLACE 1,2,NORTH");

        // Assert
        Assert.NotNull(instruction);
        Assert.IsType<PlaceCommand>(instruction);
        var placeCommand = (PlaceCommand)instruction;
        Assert.Equal(1, placeCommand.X);
        Assert.Equal(2, placeCommand.Y);
        Assert.Equal(Direction.North, placeCommand.Direction);
    }

    [Fact]
    public void Parse_PlaceWithoutDirectionCommand_ReturnsPlaceCommand()
    {
        // Arrange
        var parser = new InputParser();

        // Act
        var instruction = parser.Parse("PLACE 3,4");

        // Assert
        Assert.NotNull(instruction);
        Assert.IsType<PlaceCommand>(instruction);
        var placeCommand = (PlaceCommand)instruction;
        Assert.Equal(3, placeCommand.X);
        Assert.Equal(4, placeCommand.Y);
        Assert.Null(placeCommand.Direction);
    }

    [Theory]
    [InlineData("PLACE 1,2,NORTH")]
    [InlineData("place 1,2,north")]
    [InlineData("Place 1,2,North")]
    public void Parse_PlaceCommand_IsCaseInsensitive(string input)
    {
        // Arrange
        var parser = new InputParser();

        // Act
        var instruction = parser.Parse(input);

        // Assert
        Assert.NotNull(instruction);
        Assert.IsType<PlaceCommand>(instruction);
    }

    [Fact]
    public void Parse_MoveCommand_ReturnsMoveCommand()
    {
        // Arrange
        var parser = new InputParser();

        // Act
        var instruction = parser.Parse("MOVE");

        // Assert
        Assert.NotNull(instruction);
        Assert.IsType<MoveCommand>(instruction);
    }

    [Fact]
    public void Parse_LeftCommand_ReturnsLeftCommand()
    {
        // Arrange
        var parser = new InputParser();

        // Act
        var instruction = parser.Parse("LEFT");

        // Assert
        Assert.NotNull(instruction);
        Assert.IsType<LeftCommand>(instruction);
    }

    [Fact]
    public void Parse_RightCommand_ReturnsRightCommand()
    {
        // Arrange
        var parser = new InputParser();

        // Act
        var instruction = parser.Parse("RIGHT");

        // Assert
        Assert.NotNull(instruction);
        Assert.IsType<RightCommand>(instruction);
    }

    [Fact]
    public void Parse_ReportQuery_ReturnsReportQuery()
    {
        // Arrange
        var parser = new InputParser();

        // Act
        var instruction = parser.Parse("REPORT");

        // Assert
        Assert.NotNull(instruction);
        Assert.IsType<ReportQuery>(instruction);
    }

    [Theory]
    [InlineData("REPORT")]
    [InlineData("report")]
    [InlineData("Report")]
    public void Parse_ReportQuery_IsCaseInsensitive(string input)
    {
        // Arrange
        var parser = new InputParser();

        // Act
        var instruction = parser.Parse(input);

        // Assert
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
    public void Parse_InvalidCommand_ReturnsNull(string input)
    {
        // Arrange
        var parser = new InputParser();

        // Act
        var instruction = parser.Parse(input);

        // Assert
        Assert.Null(instruction);
    }

    [Theory]
    [InlineData("  PLACE 1,2,NORTH  ")]
    [InlineData("PLACE  1,2,NORTH")]
    [InlineData("  MOVE  ")]
    [InlineData("  REPORT  ")]
    public void Parse_CommandWithExtraWhitespace_ParsesCorrectly(string input)
    {
        // Arrange
        var parser = new InputParser();

        // Act
        var instruction = parser.Parse(input);

        // Assert
        Assert.NotNull(instruction);
    }
}
