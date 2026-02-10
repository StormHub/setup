using Simulator.Commands;
using Simulator.Queries;

namespace Simulator;

public class RobotSimulator
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
