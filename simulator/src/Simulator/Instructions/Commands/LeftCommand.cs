using Simulator.Robots;

namespace Simulator.Instructions.Commands;

internal record LeftCommand : ICommand
{
    public void Execute(Robot robot) => robot.Left();
}
