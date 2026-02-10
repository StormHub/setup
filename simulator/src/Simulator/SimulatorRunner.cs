using Microsoft.Extensions.Logging;
using Simulator.Instructions;
using Simulator.Instructions.Commands;
using Simulator.Instructions.Queries;
using Simulator.Robots;

namespace Simulator;

internal sealed class SimulatorRunner(
    RobotSimulator simulator, 
    InputParser parser, 
    ILogger<SimulatorRunner> logger)
{
    public void Run(CancellationToken token)
    {
        foreach (var instruction in Read(token))
        {
            switch (instruction)
            {
                case ICommand command:
                    simulator.Execute(command);
                    break;
            
                case IQuery query:
                    var result = simulator.Query(query);
                    Console.WriteLine(result);
                    break;
                
                default:
                    logger.LogWarning("Unknown instruction type: {Type}", instruction.GetType().Name);
                    break;
            }
        }
    }
    
    private IEnumerable<IInstruction> Read(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                continue;

            var instruction = parser.Parse(input);
            if (instruction == null)
            {
                logger.LogWarning("Invalid input: {Input}", input);
                continue;
            }
            
            yield return instruction;
        }
    }
}