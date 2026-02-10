using Simulator.Robots;

namespace Simulator.Instructions.Commands;

internal interface ICommand : IInstruction
{
    void Execute(Robot robot);
}
