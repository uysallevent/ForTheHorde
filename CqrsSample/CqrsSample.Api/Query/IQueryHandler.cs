using CqrsSample.Api.Query;

namespace CqrsSample.Api.Query
{
    public interface IQueryHandler<in TQuery,out TResult> where TQuery : IQuery
    {
        TResult Query(TQuery query);
    }
}
