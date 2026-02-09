namespace App.Shared.Messaging;

public interface IMediator
{
    public Task Execute<TCommand>(TCommand command, CancellationToken token = default)
        where TCommand : ICommand;
    
    public Task<TResponse> Query<TResponse>(IQuery<TResponse> query, CancellationToken token = default)
        where TResponse : IQueryResponse;
}