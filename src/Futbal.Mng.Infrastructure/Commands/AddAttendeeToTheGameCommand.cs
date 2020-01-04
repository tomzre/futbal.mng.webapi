using System;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;

namespace Futbal.Mng.Infrastructure.Commands
{
    public class AddAttendeeToTheGameCommand: ICommand
    {
        public Guid GameId { get; set; }

        public Guid NewAttendeeId { get; set; }
    }
}