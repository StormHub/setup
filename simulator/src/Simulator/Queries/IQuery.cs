namespace Simulator.Queries;

public interface IQuery : IInstruction
{
    string Execute(Robot robot);
}
