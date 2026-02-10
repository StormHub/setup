namespace Simulator.Robots;

internal sealed class Robot(Table table)
{
    private RobotState? _state;

    public bool IsPlaced => _state.HasValue;

    public bool Place(Position position, Direction direction)
    {
        if (!table.IsValidPosition(position))
            return false;

        _state = new RobotState(position, direction);
        return true;
    }

    public bool Place(Position position)
    {
        if (!IsPlaced || !table.IsValidPosition(position))
            return false;

        _state = _state!.Value with { Position = position };
        return true;
    }

    public bool Move()
    {
        if (!IsPlaced)
            return false;

        var newPosition = _state!.Value.Position.Move(_state.Value.Direction);

        if (!table.IsValidPosition(newPosition))
            return false;

        _state = _state.Value with { Position = newPosition };
        return true;
    }

    public bool Left()
    {
        if (!IsPlaced)
            return false;

        _state = _state!.Value with { Direction = _state.Value.Direction.RotateLeft() };
        return true;
    }

    public bool Right()
    {
        if (!IsPlaced)
            return false;

        _state = _state!.Value with { Direction = _state.Value.Direction.RotateRight() };
        return true;
    }

    public string Report()
    {
        return IsPlaced ? _state!.Value.ToString() : string.Empty;
    }
}
