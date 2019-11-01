using System;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;

namespace Futbal.Mng.Infrastructure.Commands
{
    public class CreateNewGameCommand : ICommand
    {
        public string Name { get; set; }
        public DateTime GameDate { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public Guid OwnerId { get; set; }
    }
}