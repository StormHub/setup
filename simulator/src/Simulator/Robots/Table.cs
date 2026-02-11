using System.Diagnostics.CodeAnalysis;

namespace Simulator.Robots;

internal sealed class Table
{
    private const int DefaultSize = 6;

    internal int Size { get; }

    public Table(int size = DefaultSize)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);
        Size = size;
    }
    
    internal sealed class Position
    {
        public int X { get; }
        
        public int Y { get; }

        private Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal static bool TryCreate(Table table, int x, int y, [NotNullWhen(true)] out Position? position)
        {
            if (x >= 0 && x < table.Size && y >= 0 && y < table.Size)
            {
                position = new Position(x, y);
                return true;
            }

            position = null;
            return false;
        }
    }

    public bool TryPlace(int x, int y, [NotNullWhen(true)] out Position? position) => 
        Position.TryCreate(this, x, y, out position);
}
