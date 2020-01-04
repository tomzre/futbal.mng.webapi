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

        public async Task<T> Dispatch<T>(IQuery<T> query)
        {
            if(query == null)
            {
                throw new ArgumentNullException(nameof(query), $"Command: '{typeof(T).Name} cannot be null'");
            }
            var handler = _componentContext.Resolve<IHandleQuery<IQuery<T>, T>>();
            return await handler.HandleAsync(query);
        }
    }
}