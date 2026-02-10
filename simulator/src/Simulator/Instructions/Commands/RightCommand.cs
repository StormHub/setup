using Simulator.Robots;

namespace Simulator.Instructions.Commands;

internal record RightCommand : ICommand
{
    public void Execute(Robot robot) => robot.TryTurnRight();
}
