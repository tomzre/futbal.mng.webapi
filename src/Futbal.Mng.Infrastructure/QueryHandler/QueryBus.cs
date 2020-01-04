using System;
using System.Threading.Tasks;
using Autofac;
using Futbal.Mng.Infrastructure.Interfaces.QueryHandler;

namespace Futbal.Mng.Infrastructure.QueryHandler
{
    public class QueryBus : IQueryBus
    {
        private readonly IComponentContext _componentContext;

        public QueryBus(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public async Task<TResult> DispatchAsync<TQuery, TResult>(TQuery query) where TQuery: IQuery<TResult>
        {
            if(query == null)
            {
                throw new ArgumentNullException(nameof(query), $"Command: '{typeof(TQuery).Name} cannot be null'");
            }

            var handler = _componentContext.Resolve<IHandleQuery<TQuery, TResult>>();
            return await handler.HandleAsync(query);
        }
    }
}