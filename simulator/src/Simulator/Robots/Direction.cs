using System.Diagnostics.CodeAnalysis;

namespace Simulator.Robots;

public sealed class Direction : IEquatable<Direction>
{
    public static readonly Direction North = new("NORTH", 0, 1);
    public static readonly Direction East = new("EAST", 1, 0);
    public static readonly Direction South = new("SOUTH", 0, -1);
    public static readonly Direction West = new("WEST", -1, 0);

    // Directions ordered clockwise
    private static readonly Direction[] Directions = [North, East, South, West];

    private readonly string _name;
    private readonly int _deltaX;
    private readonly int _deltaY;

    private Direction(string name, int deltaX, int deltaY)
    {
        _name = name;
        _deltaX = deltaX;
        _deltaY = deltaY;
    }

    public Direction RotateLeft() => Directions[(Array.IndexOf(Directions, this) + 3) % 4];

    public Direction RotateRight() => Directions[(Array.IndexOf(Directions, this) + 1) % 4];

    public (int DeltaX, int DeltaY) GetMovementDelta() => (_deltaX, _deltaY);

    public static bool TryParse(string value, [NotNullWhen(true)] out Direction? direction)
    {
        direction = Directions.FirstOrDefault(d => 
            string.Equals(d._name, value, StringComparison.OrdinalIgnoreCase));
        return direction is not null;
    }

    public override string ToString() => _name;

    public bool Equals(Direction? other) => ReferenceEquals(this, other);

    public override bool Equals(object? obj) => Equals(obj as Direction);

    public override int GetHashCode() => _name.GetHashCode();

    public static bool operator ==(Direction? left, Direction? right) => 
        ReferenceEquals(left, right);

    public static bool operator !=(Direction? left, Direction? right) => 
        !ReferenceEquals(left, right);
}
