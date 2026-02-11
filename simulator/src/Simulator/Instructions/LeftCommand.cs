using Simulator.Robots;

namespace Simulator.Instructions;

internal record LeftCommand : IInstruction
{
    public void Execute(Robot robot) => robot.TryTurnLeft();
}
