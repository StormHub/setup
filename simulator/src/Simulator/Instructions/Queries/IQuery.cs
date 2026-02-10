using Simulator.Robots;

namespace Simulator.Instructions.Queries;

internal interface IQuery : IInstruction
{
    string Execute(Robot robot);
}
