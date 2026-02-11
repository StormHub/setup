using Microsoft.Extensions.Logging;
using Simulator.Robots;

namespace Simulator.Instructions;

internal record RightCommand : IInstruction
{
    private readonly ILogger _logger;
    
    public RightCommand(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<RightCommand>();
    }

    public void Execute(Robot robot)
    {
        if (!robot.TryTurnRight())
        {
            _logger.LogDebug("Right command ignored: unable to turn right {Robot}", robot.Report());
        }
    }
}
