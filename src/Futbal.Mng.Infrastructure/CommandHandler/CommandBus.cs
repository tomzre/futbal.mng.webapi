using System;
using System.Threading.Tasks;
using Autofac;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;

namespace Futbal.Mng.Infrastructure.CommandHandler
{
    public class CommandBus : ICommandBus
    {
        private readonly IComponentContext _componentContext;

        public CommandBus(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }
        public void SendCommand<T>(T cmd) where T : ICommand
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd), $"Command: '{typeof(T).Name} cannot be null'");
            }
            var handler = _componentContext.Resolve<IHandleCommand<T>>();
            handler.Handle(cmd);
        }

        public async Task SendCommandAsync<T>(T cmd) where T : ICommand
        {
            if(cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd), $"Command: '{typeof(T).Name} cannot be null'");
            }
            var handler = _componentContext.Resolve<IHandleCommand<T>>();
            await handler.HandleAsync(cmd);
        }
    }
}