using BDStore.Domain.Abstraction;
using FluentValidation.Results;
using MediatR;

namespace BDStore.Application.Abstractions.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task PublishEvent<T>(T evento) where T : Event
    {
        await _mediator.Publish(evento);
    }

    public async Task<ValidationResult> SendCommand<T>(T comand) where T : Command
    {
        return await _mediator.Send(comand);
    }
}