using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Simulator.Instructions;
using Simulator.Robots;

namespace Simulator;

internal sealed class SimulatorRunner(RobotSimulator simulator, InputParser parser, ILogger<SimulatorRunner>? logger)
{
    private readonly ILogger _logger = logger ?? NullLogger<SimulatorRunner>.Instance;

    public void Run(TextReader input, CancellationToken token = default)
    {
        foreach (var instruction in Read(input, token))
        {
            simulator.Execute(instruction);
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

            if (!parser.TryParse(line, out var instruction))
            {
                _logger.LogDebug("Invalid input: {Input}", line);
                continue;
            }
            
            yield return instruction;
        }
    }
}