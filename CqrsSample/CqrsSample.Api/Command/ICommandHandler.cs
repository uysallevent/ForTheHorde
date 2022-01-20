using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CqrsSample.Api.Command
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}
