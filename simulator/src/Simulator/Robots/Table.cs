namespace Simulator.Robots;

internal sealed class Table(int size = Table.DefaultSize)
{
    private const int DefaultSize = 6;

    internal int Size { get; } = size;

    public bool IsValidPosition(Position position) => position.X >= 0 && position.X < Size &&
                                                      position.Y >= 0 && position.Y < Size;
}
