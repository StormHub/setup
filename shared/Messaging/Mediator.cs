using Microsoft.Extensions.DependencyInjection;

namespace Messaging;

internal sealed class Mediator(IServiceProvider provider) : IMediator
{
    public async Task Execute<TCommand>(TCommand command, CancellationToken token = default)
        where TCommand : ICommand
    {
        var handler = provider.GetRequiredService<ICommandHandler<TCommand>>();
        await handler.Handle(command, token);
    }

    public async Task<TResponse> Query<TResponse>(IQuery<TResponse> query, CancellationToken token = default)
        where TResponse : IQueryResponse
    {
        var queryType = query.GetType();
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResponse));
        var handler = provider.GetRequiredService(handlerType);

        var method = handlerType.GetMethod("Handle")!;
        var task = (Task<TResponse>)method.Invoke(handler, [query, token])!;
        return await task;
    }
}
