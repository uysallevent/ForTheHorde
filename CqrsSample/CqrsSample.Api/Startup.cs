using CqrsSample.Api.Command;
using CqrsSample.Api.Context;
using CqrsSample.Api.IoC;
using CqrsSample.Api.Models;
using CqrsSample.Api.Query;
using CqrsSample.Api.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace CqrsSample.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<BaseContext>(options => options.UseInMemoryDatabase("TestDb"));
            services.AddSingleton<IWriteRepository<Task>, BaseRepository<Task>>();
            services.AddSingleton<IReadRepository<Task>, BaseRepository<Task>>();

            services.AddSingleton<IContainer, SimpleIocContainer>();
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<ICommandHandler<CreateTaskCommand>, CreateTaskCommandHandler>();
            services.AddSingleton<IQueryDispatcher<GetTasksQuery,Task>, QueryDispatcher<GetTasksQuery, Task>>();
            services.AddSingleton<IQueryHandler<GetTasksQuery, IQueryable<Task>>, GetTasksQueryHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            using (var service = app.ApplicationServices.CreateScope())
            {
                var context = service.ServiceProvider.GetService<BaseContext>();
                context.Database.EnsureCreated();
            }
        }
    }
}
