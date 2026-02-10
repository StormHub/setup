namespace Simulator.Commands;

public record LeftCommand : ICommand
{
    public void Execute(Robot robot) => robot.Left();
}
