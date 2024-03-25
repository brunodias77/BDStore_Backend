using BDStore.Domain.Abstraction;
using FluentValidation.Results;
using MediatR;

namespace BDStore.Application.Clients.RegisterClient;

public class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, ValidationResult>
{
    public Task<ValidationResult> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        if(!message.isVali())
    }
}