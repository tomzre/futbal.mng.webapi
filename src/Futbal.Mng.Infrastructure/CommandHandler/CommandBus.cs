using System;
using System.Threading.Tasks;
using Autofac;
using Futbal.Mng.Infrastructure.EF;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;

namespace Futbal.Mng.Infrastructure.CommandHandler {
    public class CommandBus : ICommandBus {
        private readonly IComponentContext _componentContext;
        private readonly IUnitOfWork _unitOfWork;

        public CommandBus (IComponentContext componentContext, IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
            _componentContext = componentContext;
        }

        public async Task SendCommandAsync<T> (T cmd) where T : ICommand {
            if (cmd == null) {
                throw new ArgumentNullException (nameof (cmd), $"Command: '{typeof(T).Name} cannot be null'");
            }
            var handler = _componentContext.Resolve<IHandleCommand<T>> ();
            await handler.HandleAsync (cmd);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}