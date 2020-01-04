using System.Threading.Tasks;
using Futbal.Mng.Infrastructure.QueryHandler;

namespace Futbal.Mng.Infrastructure.Interfaces.QueryHandler
{
    public interface IQueryBus
    {
         Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery: IQuery<TResult> where TResult: IQueryResult;
    }
}