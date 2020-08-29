using System.Threading.Tasks;
using Futbal.Mng.Infrastructure.Interfaces.EventHandler;

namespace Futbal.Mng.Infrastructure.EventHandlers
{
    public class UserCreatedEventHandler : IIntegrationEventHandler<UserCreatedEvent>
    {
        public Task Handle(UserCreatedEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}