using Futbal.Mng.Domain.Event.Events;
using Futbal.Mng.Infrastructure.Interfaces.EventHandler;

namespace Futbal.Mng.Infrastructure.EventHandler.Events
{
    public class GameCreatedEventHandler : IHandleEvent<GameCreatedEvent>
    {
        public void Handle(GameCreatedEvent domainEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}