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
            services.AddSingleton<IRabbitMqPersistentConnection>(sp => {
                var factory = new ConnectionFactory{ HostName = "localhost", Password = "guest", UserName= "guest", Port = 5672 };

                return new DefaultRabbitMqPersistentConnection(factory);
            });

            return services;
        }
    }
}