using Autofac;
using Futbal.Mng.Infrastructure.EventBus;
using Futbal.Mng.Infrastructure.Interfaces.EventBus;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace futbal.mng.auth_identity.Extensions
{
    public static class RabbitExtensions
    {
        public static IServiceCollection AddRabbit(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
            {
                var factory = new ConnectionFactory { HostName = "localhost", Password = "guest", UserName = "guest", Port = 5672 };
                System.Console.WriteLine($"rabbitmq host: {factory.HostName}");
                return new DefaultRabbitMqPersistentConnection(factory);
            });

            return services;
        }

        public static IServiceCollection RegisterEventBus(this IServiceCollection services)
        { 
            services.AddSingleton<IEventBus, EventBusRabbitMq>(sp =>
            {
                var persistentConnection = sp.GetRequiredService<IRabbitMqPersistentConnection>();
                var lifeTimeScope = sp.GetRequiredService<ILifetimeScope>();
                return new EventBusRabbitMq(persistentConnection, lifeTimeScope);
            });

            return services;
        }
    }
}