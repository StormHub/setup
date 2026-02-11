using Simulator.Robots;

namespace Simulator.Instructions;

internal record MoveCommand : IInstruction
{
    public void Execute(Robot robot) => robot.TryMove();
}
