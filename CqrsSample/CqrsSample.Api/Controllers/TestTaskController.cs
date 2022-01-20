using CqrsSample.Api.Context;
using CqrsSample.Api.Models;
using CqrsSample.Api.Query;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CqrsSample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTaskController : ControllerBase
    {
        private readonly IQueryDispatcher<GetTasksQuery, Task> _queryDispatcher;
        public TestTaskController(IQueryDispatcher<GetTasksQuery, Task> queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet()]
        public IActionResult GetAllTask()
        {
            var getTasksQuery = new GetTasksQuery();
            getTasksQuery.Predicate = (t) => t.IsCompleted == false;
            IQueryable<Task> tasks = _queryDispatcher.Query(getTasksQuery);

            return Ok();
        }
    }
}
