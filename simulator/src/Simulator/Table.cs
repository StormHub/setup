namespace Simulator;

public sealed class Table
{
    private const int MinX = 0;
    private const int MinY = 0;
    private const int MaxX = 5;
    private const int MaxY = 5;

    public bool IsValidPosition(Position position)
    {
        return position.X >= MinX && position.X <= MaxX &&
               position.Y >= MinY && position.Y <= MaxY;
    }
}
