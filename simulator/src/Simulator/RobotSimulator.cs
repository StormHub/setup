using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Simulator.Instructions;
using Simulator.Robots;

namespace Simulator;

internal sealed class RobotSimulator(InputParser parser, ILogger<RobotSimulator>? logger = null)
{
    private readonly Robot _robot = new(new Table());
    private readonly ILogger _logger = logger ?? NullLogger<RobotSimulator>.Instance;

    public void Run(TextReader input, CancellationToken token = default)
    {
        foreach (var instruction in Read(input, token))
        {
            Execute(instruction);
        }
    }

    internal void Execute(params IEnumerable<IInstruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            instruction.Execute(_robot);
        }
    }

    internal string Report() => _robot.Report();

    private IEnumerable<IInstruction> Read(TextReader input, CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            var line = input.ReadLine();
            if (line == null) break;

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
