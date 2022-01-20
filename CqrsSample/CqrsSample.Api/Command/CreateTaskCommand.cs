using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CqrsSample.Api.Command
{
    public class CreateTaskCommand:ICommand
    {
        public string Title { get; set; }
        public string UserName { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
