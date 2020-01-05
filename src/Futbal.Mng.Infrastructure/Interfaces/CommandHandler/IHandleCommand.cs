using System.Threading.Tasks;

namespace Futbal.Mng.Infrastructure.Interfaces.CommandHandler
{
    public interface IHandleCommand
    {
         
    }

    public interface IHandleCommand<TCommand> : IHandleCommand where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}