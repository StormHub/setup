using Microsoft.Extensions.Logging;
using Simulator.Instructions;
using Simulator.Instructions.Commands;
using Simulator.Instructions.Queries;
using Simulator.Robots;

namespace Simulator;

internal sealed class ConsoleSimulator(RobotSimulator simulator, InputParser parser, ILogger<ConsoleSimulator> logger)
{
    private readonly ILogger _logger = logger;

    public void Run(CancellationToken token)
    {
        Console.WriteLine("Toy Robot Simulator");
        Console.WriteLine("==================");
        Console.WriteLine("Commands: PLACE X,Y,DIRECTION | PLACE X,Y | MOVE | LEFT | RIGHT | REPORT");
        Console.WriteLine();

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
                    _logger.LogWarning("Unknown instruction type: {Type}", instruction.GetType().Name);
                    break;
            }
        }
        
        Console.WriteLine("Goodbye!");
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
                _logger.LogWarning("Invalid input: {Input}", input);
                continue;
            }
            
            yield return instruction;
        }
    }
}