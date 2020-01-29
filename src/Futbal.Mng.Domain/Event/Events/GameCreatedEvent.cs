using System;

namespace Futbal.Mng.Domain.Event.Events
{
    public class GameCreatedEvent : IDomainEvent
    {
        public Guid Id { get; private set; }

        public string OwnerName { get; private set; }

        public string GameName { get; private set; }

        public GameCreatedEvent(Guid id, string ownerName, string gameName)
        {
            Id = id;
            OwnerName = ownerName;
            GameName = gameName;
        }
    }
}