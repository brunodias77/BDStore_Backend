using BDStore.Domain.Abstraction;
using FluentValidation.Results;

namespace BDStore.Application.Abstractions.Mediator;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T evento) where T : Event;
    Task<ValidationResult> SendCommand<T>(T comando) where T : Command;
}