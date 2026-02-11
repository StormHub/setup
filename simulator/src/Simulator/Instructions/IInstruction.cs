using Simulator.Robots;

namespace Simulator.Instructions;

internal interface IInstruction
{
    void Execute(Robot robot);
}
