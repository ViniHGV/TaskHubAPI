using Microsoft.EntityFrameworkCore;
using TaskHubAPI.Models;

namespace TaskHubAPI.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost; Database=TaskHub; Username=postgres; Password=123");
    }
}