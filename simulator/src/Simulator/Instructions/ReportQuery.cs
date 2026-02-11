using Microsoft.Extensions.Logging;
using Simulator.Robots;

namespace Simulator.Instructions;

internal sealed class ReportQuery(TextWriter output, ILoggerFactory loggerFactory) : IInstruction
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<ReportQuery>();

    public void Execute(Robot robot)
    {
        // If the robot has not been placed on the table, ignore the command.
        if (!robot.IsPlaced)
        {
            _logger.LogDebug("Report command ignored: robot has not been placed on the table");
            return;
        }
        
        output.WriteLine(robot.Report());
    }
}
