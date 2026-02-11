using Simulator.Robots;

namespace Simulator.Instructions;

internal sealed class ReportQuery(TextWriter output) : IInstruction
{
    public void Execute(Robot robot) => output.WriteLine(robot.Report());
}
