using MediatR;

namespace BDStore.Application.Abstractions.Commands;

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : IRequest<TResponse>
{
    Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken);
}