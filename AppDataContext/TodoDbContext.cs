// AppDataContext/TodoDbContext.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoAPI.Models;

namespace TodoAPI.AppDataContext
{
    // TodoDbContext class inherits from DbContext
    public class TodoDbContext : DbContext
    {
        // DbSettings field to store the connection string
        private readonly DbSettings _dbSettings;

        // Constructor to inject the DbSettings model
        public TodoDbContext(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        // DbSet property to represent the Todo table
        public DbSet<Todo> Todos { get; set; }

        // Configuring the database provider and connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
        }

        // Configuring the model for the Todo entity
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .ToTable("TodoAPI")
                .HasKey(x => x.Id);
        }
    }
}