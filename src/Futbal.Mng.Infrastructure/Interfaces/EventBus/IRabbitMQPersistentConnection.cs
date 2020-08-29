using RabbitMQ.Client;

namespace Futbal.Mng.Infrastructure.Interfaces.EventBus
{
    public interface IRabbitMqPersistentConnection
    {
        bool IsConnected { get; }   

        bool TryConnect();

        IModel CreateModel();
    }
}