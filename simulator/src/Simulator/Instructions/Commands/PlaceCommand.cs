using Simulator.Robots;

namespace Simulator.Instructions.Commands;

internal record PlaceCommand(Position Position, Direction? Direction) : ICommand
{
    public void Execute(Robot robot)
    {
        if (Direction.HasValue)
        {
            robot.Place(Position, Direction.Value);
        }
        else
        {
            robot.Place(Position);
        }
    }
}
