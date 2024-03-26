using BDStore.Application.Abstractions.Commands;
using BDStore.Application.Response;
using BDStore.Domain.DomainObjects;
using FluentValidation;
using MediatR;

namespace BDStore.Application.Clients.RegisterClient;

public class RegisterClientCommand : Command, IRequest<ApiResponse<string>>
{
    public RegisterClientCommand(Guid id, string name, string email, string cpf)
    {
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public override bool IsValid()
    {
        ValidationResult = new RegisterClientCommandValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RegisterClientCommandValidation : AbstractValidator<RegisterClientCommand>
{
    public RegisterClientCommandValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("O Id do cliente é invalido");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("O nome do clinte não foi informado");

        RuleFor(c => c.Cpf)
            .Must(IsValidCpf)
            .WithMessage("CPF invalido.");

        RuleFor(c => c.Email)
            .Must(HasValidEmail)
            .WithMessage("O e-mail informado não é válido.");
    }

    protected static bool IsValidCpf(string cpf)
    {
        return Cpf.Validate(cpf);
    }

    protected static bool HasValidEmail(string email)
    {
        return Email.Validate(email);
    }
}