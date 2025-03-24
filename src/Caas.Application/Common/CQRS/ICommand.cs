namespace Caas.Application.Common.CQRS;

public interface ICommand : ICommand<Unit>, IRequest
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>, IRequest
{
}
