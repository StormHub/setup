namespace Simulator.Commands;

public record PlaceCommand(Position Position, Direction? Direction) : ICommand
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
