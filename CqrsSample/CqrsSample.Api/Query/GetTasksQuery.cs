using CqrsSample.Api.Models;
using System;
using System.Linq.Expressions;

namespace CqrsSample.Api.Query
{
    public class GetTasksQuery : IQuery
    {
        public Expression<Func<Task, bool>> Predicate { get; set; }

    }
}
