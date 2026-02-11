using Microsoft.Extensions.Logging;
using Simulator.Robots;

namespace Simulator.Instructions;

internal record LeftCommand : IInstruction
{
    private readonly ILogger _logger;
    
    public LeftCommand(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<MoveCommand>();
    }

    public void Execute(Robot robot)
    {
        if (!robot.TryTurnLeft())
        {
            _logger.LogDebug("Left command ignored: unable to turn left {Robot}", robot.Report());
        }
    }
}
