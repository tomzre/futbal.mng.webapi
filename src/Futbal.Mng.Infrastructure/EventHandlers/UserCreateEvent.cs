using RabbitMQ.Client;

namespace Futbal.Mng.Infrastructure.EventHandlers
{
    public class UserCreateEvent
    {
        private readonly IModel _channel;

        public UserCreateEvent(IModel channel)
        {
            _channel = channel;
        }        
    }
}