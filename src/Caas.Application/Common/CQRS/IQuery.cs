namespace Caas.Application.Common.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>, IRequest
    where TResponse : notnull
{
}
