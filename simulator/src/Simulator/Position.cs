namespace Simulator;

public readonly record struct Position(int X, int Y)
{
    public Position Move(Direction direction)
    {
        var (deltaX, deltaY) = direction.GetMovementDelta();
        return new Position(X + deltaX, Y + deltaY);
    }
}
