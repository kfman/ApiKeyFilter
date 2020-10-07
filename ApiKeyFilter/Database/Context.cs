using ApiKeyFilter.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiKeyFilter.Database {
    public class Context : DbContext {
        private readonly string _connectionString;

        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }

        public Context() : this("Data Source=../apiKeys.sqlite") {
        }

        public Context(string connectionString) {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(this._connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApiKey>()
                .HasMany(a => a.Roles)
                .WithOne(ar => ar.ApiKey)
                .HasForeignKey(ar => ar.ApiKeyId);
            
            modelBuilder.Entity<Role>()
                .HasMany(a => a.ApiKeys)
                .WithOne(ar => ar.Role)
                .HasForeignKey(ar => ar.RoleId);
            
            modelBuilder.Entity<ApiKeyRoles>()
                .HasKey(a => new {a.ApiKeyId, a.RoleId});
        }
    }
}
