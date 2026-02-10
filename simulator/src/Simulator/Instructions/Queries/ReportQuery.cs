using Simulator.Robots;

namespace Simulator.Instructions.Queries;

internal sealed class ReportQuery : IQuery
{
    public string Execute(Robot robot) => robot.Report();
}
