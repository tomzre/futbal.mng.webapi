using System.Threading.Tasks;

namespace Futbal.Mng.Infrastructure.Interfaces.CommandHandler
{
    public interface ICommandBus
    {
         Task SendCommandAsync<T>(T cmd) where T: ICommand;
    }
}