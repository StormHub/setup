using Simulator.Robots;

namespace Simulator.Instructions;

internal record RightCommand : IInstruction
{
    public void Execute(Robot robot) => robot.TryTurnRight();
}
