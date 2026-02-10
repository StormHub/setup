namespace Simulator.Commands;

public record MoveCommand : ICommand
{
    public void Execute(Robot robot) => robot.Move();
}
