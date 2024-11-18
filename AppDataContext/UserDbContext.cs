// AppDataContext/UserDbContext.cs

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TodoAPI.Models;

namespace TodoAPI.AppDataContext
{
    // TodoDbContext class inherits from DbContext
    public class UserDbContext : DbContext
    {
        // DbSettings field to store the connection string
        private readonly DbSettings _dbSettings;

        // Constructor to inject the DbSettings model
        public UserDbContext(IOptions<DbSettings> dbSettings)
        {
            _dbSettings = dbSettings.Value;
        }

        // DbSet property to represent the table
        public DbSet<User> Users { get; set; }

        // Configuring the database provider and connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbSettings.UserAPI.ConnectionString);
        }

        // Configuring the model for the User entity
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("UserAPI")
                .HasKey(x => x.Id);
        }
    }
}