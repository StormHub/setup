namespace Messaging;

public interface IQueryHandler<in TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : IQueryResponse
{
    public Task<TResponse> Handle(TQuery query, CancellationToken token = default);
}