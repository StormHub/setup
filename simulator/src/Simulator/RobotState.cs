namespace Simulator;

public readonly record struct RobotState(Position Position, Direction Direction)
{
    public override string ToString() => $"{Position.X},{Position.Y},{Direction.ToString().ToUpper()}";
}
