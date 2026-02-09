namespace App.Shared.Messaging;

public interface IQuery<T>
    where T : IQueryResponse
{
}