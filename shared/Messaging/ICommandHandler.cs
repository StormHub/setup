namespace Messaging;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
    public Task Handle(TCommand command, CancellationToken token = default);
}