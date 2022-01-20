using System.Linq;

namespace CqrsSample.Api.Query
{
    public interface IQueryDispatcher<TQuery,T> where TQuery : IQuery
    {
        IQueryable<T> Query(TQuery query);
    }
}
