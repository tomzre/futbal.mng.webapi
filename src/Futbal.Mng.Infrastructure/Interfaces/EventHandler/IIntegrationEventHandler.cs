using System.Threading.Tasks;

namespace Futbal.Mng.Infrastructure.Interfaces.EventHandler
{
    public interface IIntegrationEventHandler<TEvent> where TEvent : IIntegrationEvent
    {
        Task Handle(TEvent @event);
    }
}