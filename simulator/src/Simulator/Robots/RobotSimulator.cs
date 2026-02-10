using Simulator.Instructions.Commands;
using Simulator.Instructions.Queries;

namespace Simulator.Robots;

internal sealed class RobotSimulator
{
    private readonly Robot _robot = new(new Table());

    public void Execute(params IEnumerable<ICommand> commands)
    {
        foreach (var command in commands)
        {
            command.Execute(_robot);
        }
    }

    public string Query(IQuery query) => query.Execute(_robot);

    public string Report() => _robot.Report();
}
