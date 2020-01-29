using Futbal.Mng.Domain.Event;

namespace Futbal.Mng.Infrastructure.Interfaces.EventHandler
{
    public interface IHandleEvent<TEvent> where TEvent: IDomainEvent
    {
        void Handle(TEvent domainEvent);
    }
}