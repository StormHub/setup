using Microsoft.Extensions.Logging;
using Simulator.Robots;

namespace Simulator.Instructions;

internal record PlaceCommand : IInstruction
{
    private readonly ILogger _logger;
    
    internal int X { get; }

    internal int Y { get; }

    internal Direction? Direction { get; }

    public PlaceCommand(int x, int y, Direction? direction, ILoggerFactory loggerFactory)
    {
        X = x;
        Y = y;
        Direction = direction;
        _logger = loggerFactory.CreateLogger<PlaceCommand>();
    }
    
    public void Execute(Robot robot)
    {
        var result = Direction is not null
            ? robot.TryPlace(X, Y, Direction)
            : robot.TryPlace(X, Y);
        
        if (!result)        
        {
            _logger.LogDebug("Place command ignored: unable to place {Robot} at ({X}, {Y}, {Direction})", 
                robot.Report(), X, Y, Direction);
        }
    }
}
