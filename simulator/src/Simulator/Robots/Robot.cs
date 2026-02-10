namespace Simulator.Robots;

internal readonly record struct RobotState(Table.Position Position, Direction Direction)
{
    public override string ToString() => $"{Position.X},{Position.Y},{Direction.ToString().ToUpper()}";
}

internal sealed class Robot(Table table)
{
    private RobotState? _state;

    internal bool IsPlaced => _state.HasValue;

    public bool TryPlace(int x, int y, Direction direction)
    {
        if (!table.TryPlace(x, y, out var position))
            return false;

        _state = new RobotState(position, direction);
        return true;
    }

    public bool TryPlace(int x, int y)
    {
        var direction = _state?.Direction;
        if (direction is null || !table.TryPlace(x, y, out var position))
            return false;

        _state = new RobotState(position, direction);
        return true;
    }

    public bool TryMove()
    {
        if (!_state.HasValue) return false;

        var (deltaX, deltaY) = _state.Value.Direction.GetMovementDelta();
        var newX = _state.Value.Position.X + deltaX;
        var newY = _state.Value.Position.Y + deltaY;
        
        if (!table.TryPlace(newX, newY, out var newPosition))
            return false;

        _state = _state.Value with { Position = newPosition };
        return true;
    }

    public bool TryTurnLeft()
    {
        if (!_state.HasValue) return false;

        _state = _state.Value with { Direction = _state.Value.Direction.RotateLeft() };
        return true;
    }

    public bool TryTurnRight()
    {
        if (!_state.HasValue) return false;

        _state = _state.Value with { Direction = _state.Value.Direction.RotateRight() };
        return true;
    }

    public string Report() => _state.HasValue ? _state.Value.ToString() : string.Empty;
}
