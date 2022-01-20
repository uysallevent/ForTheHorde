using CqrsSample.Api.Models;
using CqrsSample.Api.Repository;
using System;

namespace CqrsSample.Api.Command
{
    public class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand>
    {
        private readonly IWriteRepository<Task> writeRepository;

        public CreateTaskCommandHandler(IWriteRepository<Task> writeRepository)
        {
            if (writeRepository == null)
            {
                throw new ArgumentNullException("writeRepository");
            }
            this.writeRepository = writeRepository;
        }

        public void Execute(CreateTaskCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            if (string.IsNullOrEmpty(command.Title))
            {
                throw new ArgumentException("Title is not specified", "command");
            }

            var task = new Task();
            task.Title = command.Title;
            task.UserName = command.UserName;
            task.IsCompleted = command.IsCompleted;
            task.CreatedDate = command.CreatedOn;
            task.LastUpdatedDate = command.UpdatedOn;

            writeRepository.Add(task);

            writeRepository.Save();
        }

    }
}
