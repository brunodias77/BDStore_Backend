using BDStore.Application.Abstractions.Commands;
using BDStore.Application.Clients.Events;
using BDStore.Domain.Clients;
using FluentValidation.Results;
using MediatR;

namespace BDStore.Application.Clients.RegisterClient;

public class RegisterClientCommandHandler : CommandHandler, IRequestHandler<RegisterClientCommand, ValidationResult>
{
    private readonly IClientRepository _clientRepository;

    public RegisterClientCommandHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<ValidationResult> Handle(RegisterClientCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var client = new Client(message.Id, message.Name, message.Email, message.Cpf);

        //Validacoes de negocio
        var clientExists = await _clientRepository.GetByCpf(client.Cpf.Number);

        //Persistir no Banco !
        if (clientExists != null) // Ja existe um cliente com o CPF informado
        {
            AddError("Este CPF ja esta em uso.");
            return ValidationResult;
        }

        _clientRepository.Add(client);
        // Lançar um evento cliente OK !
        client.AddEvent(new ClientRegistedEvent(message.Id, message.Name, message.Email, message.Cpf));
        return await PersistData(_clientRepository.UnitOfWork);
    }
}