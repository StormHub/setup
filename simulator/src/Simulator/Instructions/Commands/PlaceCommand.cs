using Simulator.Robots;

namespace Simulator.Instructions.Commands;

internal record PlaceCommand(Position Position, Direction? Direction) : ICommand
{
    public void Execute(Robot robot)
    {
        if (Direction is not null)
        {
            robot.Place(Position, Direction);
        }
        else
        {
            robot.Place(Position);
        }
    }
}
