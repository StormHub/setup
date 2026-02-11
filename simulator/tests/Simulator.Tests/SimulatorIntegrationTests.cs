using Simulator.Instructions;
using Simulator.Robots;

namespace Simulator.Tests;

public sealed class SimulatorIntegrationTests
{
    public static TheoryData<string, string> GetTestCases() => new()
    {
        {
            """
            PLACE 0,0,NORTH
            MOVE
            REPORT
            """,
            
            "0,1,NORTH"
        },
        
        {
            """
            PLACE 0,0,NORTH
            LEFT
            REPORT
            """,
            
            "0,0,WEST"
        },
        
        {
            """
            PLACE 1,2,EAST
            MOVE
            MOVE
            LEFT
            MOVE
            REPORT
            """,
            
            "3,3,NORTH"
        },
        
        {
            """
            PLACE 1,2,EAST 
            MOVE 
            LEFT 
            MOVE 
            PLACE 3,1 
            MOVE 
            REPORT
            """,
            
            "3,2,NORTH"
        }
    };
    
    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void Run(string input, string expected)
    {
        using var inputReader = new StringReader(input);
        using var outputWriter = new StringWriter();
        
        var parser = new InputParser(outputWriter, null);
        var simulator = new RobotSimulator(parser, null);
        
        simulator.Run(inputReader);
        
        Assert.Equal(expected, outputWriter.ToString().TrimEnd()); // Remove default trailing newline \n from StringWriter
    }
}
