using MediatR;

namespace BDStore.Application.Clients.Events;

public class ClientEventHandler : INotificationHandler<ClientRegistedEvent>
{
    public Task Handle(ClientRegistedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}