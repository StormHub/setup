namespace Simulator.Queries;

public sealed class ReportQuery : IQuery
{
    public string Execute(Robot robot)
    {
        return robot.Report();
    }
}
