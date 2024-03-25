using BDStore.Application.Abstractions.Commands;
using BDStore.Application.Response;
using BDStore.Domain.Abstraction;
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
}