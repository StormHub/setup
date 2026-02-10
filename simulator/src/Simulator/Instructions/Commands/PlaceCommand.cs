using Simulator.Robots;

namespace Simulator.Instructions.Commands;

internal record PlaceCommand(int X, int Y, Direction? Direction) : ICommand
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
