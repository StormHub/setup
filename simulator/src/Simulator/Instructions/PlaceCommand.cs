using Simulator.Robots;

namespace Simulator.Instructions;

internal record PlaceCommand(int X, int Y, Direction? Direction) : IInstruction
{
    public void Execute(Robot robot)
    {
        if (Direction is not null)
        {
            robot.TryPlace(X, Y, Direction);
        }
        else
        {
            robot.TryPlace(X, Y);
        }
    }
}
