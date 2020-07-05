using System;
using Futbal.Mng.Infrastructure.Interfaces.EventBus;

namespace Futbal.Mng.Infrastructure.EventBus
{
    public class EventBusRabbitMq : IEventBus, IDisposable
    {
        private readonly IRabbitMqPersistentConnection _persistenConnection;

        public EventBusRabbitMq(IRabbitMqPersistentConnection persistenConnection)
        {
            _persistenConnection = persistenConnection;
            _persistenConnection.TryConnect();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        // public void Subscribe<T, TH>()
        //     where T : IntegrationEvent
        //     where TH : IIntegrationEventHandler<T>
        // {
        //     var eventName = _subsManager.GetEventKey<T>();
        //     DoInternalSubscription(eventName);

        //     Console.WriteLine("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

        //     _subsManager.AddSubscription<T, TH>();
        //     StartBasicConsume();
        // }

    }
}