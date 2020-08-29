using System;
using Futbal.Mng.Infrastructure.Interfaces.EventHandler;
using RabbitMQ.Client;

namespace Futbal.Mng.Infrastructure.EventHandlers
{
    public class UserCreatedEvent : IIntegrationEvent
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime CreationDate { get => DateTime.UtcNow; }
    }}