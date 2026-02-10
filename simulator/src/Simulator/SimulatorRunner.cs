using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Simulator.Instructions;
using Simulator.Instructions.Commands;
using Simulator.Instructions.Queries;
using Simulator.Robots;

namespace Simulator;

internal sealed class SimulatorRunner(RobotSimulator simulator, InputParser parser, ILogger<SimulatorRunner>? logger)
{
    private readonly ILogger _logger = logger ?? NullLogger<SimulatorRunner>.Instance;

    public void Run(TextReader input, TextWriter output, CancellationToken token = default)
    {
        foreach (var instruction in Read(input, token))
        {
            switch (instruction)
            {
                case ICommand command:
                    simulator.Execute(command);
                    break;
            
                case IQuery query:
                    var result = simulator.Query(query);
                    output.WriteLine(result);
                    break;
                
                default:
                    _logger.LogWarning("Unknown instruction type: {Type}", instruction.GetType().Name);
                    break;
            }
        }
    }

    private IEnumerable<IInstruction> Read(TextReader input, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var line = input.ReadLine();
            if (line == null) break; // End 
                
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var instruction = parser.Parse(line);
            if (instruction == null)
            {
                _logger.LogWarning("Invalid input: {Input}", line);
                continue;
            }
            
            yield return instruction;
        }
    }
}