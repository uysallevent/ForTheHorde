using System.Linq;

namespace CqrsSample.Api.Query
{
    public class QueryDispatcher<TQuery, T> : IQueryDispatcher<TQuery,T> where TQuery : IQuery
    {
        private readonly IQueryHandler<TQuery, IQueryable<T>> _queryHandler;

        public QueryDispatcher(IQueryHandler<TQuery, IQueryable<T>> queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public IQueryable<T> Query(TQuery query)
        {
            return _queryHandler.Query(query);
        }
    }
}
