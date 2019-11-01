using System.Threading.Tasks;

namespace Futbal.Mng.Infrastructure.Interfaces.CommandHandler
{
    public interface ICommandBus
    {
         void SendCommand<T>(T cmd) where T: ICommand;

         Task SendCommandAsync<T>(T cmd) where T: ICommand;
    }
}