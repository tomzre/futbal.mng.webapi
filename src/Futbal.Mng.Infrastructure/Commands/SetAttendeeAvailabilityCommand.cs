using System;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;

namespace Futbal.Mng.Infrastructure.Commands
{
    public class SetAttendeeAvailabilityCommand: ICommand
    {
        public Guid GameId { get; set; }

        public Guid UserId { get; set; }
        
        public bool IsAvailable { get; set; }
    }
}