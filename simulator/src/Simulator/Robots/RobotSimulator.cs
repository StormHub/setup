using Simulator.Instructions;

namespace Simulator.Robots;

internal sealed class RobotSimulator
{
    private readonly Robot _robot;

    public RobotSimulator()
    {
        _robot = new(new Table());
    }

    public void Execute(params IEnumerable<IInstruction> instructions)
    {
        foreach (var instruction in instructions)
        {
            instruction.Execute(_robot);
        }
    }

    internal string Report() => _robot.Report();
}
