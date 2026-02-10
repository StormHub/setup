namespace Simulator.Commands;

public interface ICommand : IInstruction
{
    void Execute(Robot robot);
}
