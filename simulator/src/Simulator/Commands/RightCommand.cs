namespace Simulator.Commands;

public record RightCommand : ICommand
{
    public void Execute(Robot robot) => robot.Right();
}
