using Microsoft.EntityFrameworkCore;
using TaskHubAPI.Models;

namespace TaskHubAPI.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost; Database=mydb; Username=postgres; Password=123");
    }
}