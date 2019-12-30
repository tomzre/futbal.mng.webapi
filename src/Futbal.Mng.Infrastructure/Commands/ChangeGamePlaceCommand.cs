using System;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;

namespace Futbal.Mng.Infrastructure.Commands
{
    public class ChangeGamePlaceCommand : ICommand
    {
        public Guid GameId { get; set; }
        
        public string Street { get; set; }

        public int Number { get; set; }
    }
}