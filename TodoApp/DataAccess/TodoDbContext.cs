using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.DataAccess
{
    public class TodoDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }
      //  public DbSet<Priority> Priodrity { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TodoDb");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            


            base.OnModelCreating(modelBuilder);

        }

    }
}
