using BDStore.Application.Abstractions.Messages;

namespace BDStore.Application.Abstractions.Events;

public class Event : Message, INotification
{
    public DateTime Timestamp { get; private set; }
    protected Event()
    {
        Timestamp = DateTime.Now;
    }
}