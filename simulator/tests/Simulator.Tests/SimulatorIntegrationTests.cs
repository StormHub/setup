using Simulator.Instructions;
using Simulator.Robots;

namespace Simulator.Tests;

public sealed class SimulatorIntegrationTests
{
    public static IEnumerable<object[]> GetTestCases()
    {
        yield return
        [
            """
            PLACE 0,0,NORTH
            MOVE
            REPORT
            """,
            
            "0,1,NORTH"
        ];
        
        yield return
        [
            """
            PLACE 0,0,NORTH
            LEFT
            REPORT
            """,
            
            "0,0,WEST"
        ];
        
        yield return
        [
            """
            PLACE 1,2,EAST
            MOVE
            MOVE
            LEFT
            MOVE
            REPORT
            """,
            
            "3,3,NORTH"
        ];
        
        yield return
        [
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
        ];
    }
    
    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void Run(string input, string expected)
    {
        using var inputReader = new StringReader(input);
        using var outputWriter = new StringWriter();
        
        var simulator = new RobotSimulator();
        var parser = new InputParser(outputWriter);
        var runner = new SimulatorRunner(simulator, parser, default);
        
        runner.Run(inputReader);
        
        Assert.Equal(expected, outputWriter.ToString().TrimEnd()); // Remove default trailing newline \n from StringWriter
    }
}
