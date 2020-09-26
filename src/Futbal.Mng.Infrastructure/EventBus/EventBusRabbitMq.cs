using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Futbal.Mng.Infrastructure.Interfaces.EventBus;
using Futbal.Mng.Infrastructure.Interfaces.EventHandler;
using Futbal.Mng.Infrastructure.IoC;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Futbal.Mng.Infrastructure.EventBus
{
    public class EventBusRabbitMq : IEventBus, IDisposable
    {
        private readonly IRabbitMqPersistentConnection _persistentConnection;
        private readonly string AUTOFAC_SCOPE_NAME = "futbal_mng";

        private readonly ILifetimeScope _autofac;

        private IModel _consumerChannel;

        public EventBusRabbitMq(IRabbitMqPersistentConnection persistentConnection, ILifetimeScope autofac)
        {
            _persistentConnection = persistentConnection;
            _autofac = autofac;
            _consumerChannel = CreateConsumerChannel();
            //_persistentConnection.TryConnect();
        }

        public void Dispose()
        {
            if (_consumerChannel != null)
            {
                _consumerChannel.Dispose();
            }
        }

        public void Subscribe()
        {
            if (!_persistentConnection.IsConnected)
            {
                Console.WriteLine("Connecting");
                _persistentConnection.TryConnect();

            }
            System.Console.WriteLine("Connected");

            // using(var channel = _persistentConnection.CreateModel())
            // {
            //     channel.QueueBind(queue: "identity.user", exchange: "BROKER_NAME", routingKey: "UserCreatedEvent");
            // }

            StartBasicConsume();
        }

        private IModel CreateConsumerChannel()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            System.Console.WriteLine("Creating RabbitMQ consumer channel");

            var channel = _persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: "BROKER_NAME",
                                    type: "direct",
                                    durable: true);

            channel.QueueDeclare(queue: "identity.user",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Console.WriteLine("CREATED!!");

            channel.CallbackException += (sender, ea) =>
            {
                Console.WriteLine($"Recreating RabbitMQ consumer channel,\n {ea.Exception}");

                _consumerChannel.Dispose();
                _consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };

            return channel;
        }

        private void StartBasicConsume()
        {
            System.Console.WriteLine("Starting RabbitMQ basic consume");

            if (_consumerChannel != null)
            {
                var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

                consumer.Received += Consumer_Received;

                _consumerChannel.BasicConsume(queue: "identity.user",
                noLocal: false,
                arguments: null,
                exclusive: true,
                autoAck: false,
                consumer: consumer);
            }
            else
            {
                System.Console.WriteLine("StartBasicConsume can't call on _consumerChannel == null");
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

            try
            {
                var types = Assembly.GetAssembly(typeof(InfrastructureModule)).GetTypes();
                var handlerTypes = types.Where(x => x.GetInterfaces().Contains(typeof(IIntegrationEventHandler<>)));

                //await ProcessEvent(eventName, message);

                foreach (var handlerType in handlerTypes)
                {
                    using var scope = _autofac.BeginLifetimeScope(AUTOFAC_SCOPE_NAME);
                    var handler = scope.ResolveOptional(handlerType);
                    if (handler == null) continue;
                    var eventType = handlerType.GetGenericArguments()[0];
                    Console.WriteLine($"{eventType.Name}");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("----- ERROR Processing message \"{Message}\"", message);
            }

            // Even on exception we take the message off the queue.
            // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
            // For more information see: https://www.rabbitmq.com/dlx.html
            _consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
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