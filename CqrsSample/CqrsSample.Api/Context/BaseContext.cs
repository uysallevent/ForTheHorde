using CqrsSample.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace CqrsSample.Api.Context
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Task> Task { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>().HasData(
                new Task { Id = 1, IsCompleted = false, Title = "Test Task", CreatedDate = DateTime.Now.AddDays(-4), LastUpdatedDate = DateTime.Now, UserName = "TestUser" }
                );
            base.OnModelCreating(modelBuilder);
        }

    }
}
