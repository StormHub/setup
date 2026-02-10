namespace Simulator.Robots;

public enum Direction
{
    North,
    East,
    South,
    West
}

internal static class DirectionExtensions
{
    public static Direction RotateLeft(this Direction direction) => direction switch
    {
        Direction.North => Direction.West,
        Direction.West => Direction.South,
        Direction.South => Direction.East,
        Direction.East => Direction.North,
        _ => throw new ArgumentOutOfRangeException(nameof(direction))
    };

    public static Direction RotateRight(this Direction direction) => direction switch
    {
        Direction.North => Direction.East,
        Direction.East => Direction.South,
        Direction.South => Direction.West,
        Direction.West => Direction.North,
        _ => throw new ArgumentOutOfRangeException(nameof(direction))
    };

    public static (int DeltaX, int DeltaY) GetMovementDelta(this Direction direction) => direction switch
    {
        Direction.North => (0, 1),
        Direction.East => (1, 0),
        Direction.South => (0, -1),
        Direction.West => (-1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(direction))
    };

    public static bool TryParse(string value, out Direction direction)
    {
        return Enum.TryParse<Direction>(value, ignoreCase: true, out direction);
    }
}
