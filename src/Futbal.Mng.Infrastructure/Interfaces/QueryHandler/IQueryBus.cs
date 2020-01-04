using System.Threading.Tasks;

namespace Futbal.Mng.Infrastructure.Interfaces.QueryHandler
{
    public interface IQueryBus
    {
         Task<T> Dispatch<T>(IQuery<T> query);
         
    }
}