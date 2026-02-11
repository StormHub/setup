using Microsoft.Extensions.Logging;
using Simulator.Robots;

namespace Simulator.Instructions;

internal record MoveCommand : IInstruction
{
    private readonly ILogger _logger;
    
    public MoveCommand(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<MoveCommand>();
    }

    public void Execute(Robot robot)
    {
        if (!robot.TryMove())
        {
            _logger.LogDebug("Move command ignored: unable to move {Robot}", robot.Report());
        }
    }
}
