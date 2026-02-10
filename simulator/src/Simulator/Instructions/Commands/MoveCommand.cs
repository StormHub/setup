using Simulator.Robots;

namespace Simulator.Instructions.Commands;

internal record MoveCommand : ICommand
{
    public void Execute(Robot robot) => robot.TryMove();
}
