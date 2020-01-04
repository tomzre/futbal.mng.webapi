using System.Threading.Tasks;
using Futbal.Mng.Infrastructure.QueryHandler;

namespace Futbal.Mng.Infrastructure.Interfaces.QueryHandler
{
    public interface IHandleQuery<TQuery, TResult> where TQuery: IQuery<TResult> where TResult : IQueryResult
    {
         Task<TResult> HandleAsync(TQuery query);
    }
}