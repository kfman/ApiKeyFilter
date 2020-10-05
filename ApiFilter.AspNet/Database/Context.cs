using ApiKeyFilter.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiKeyFilter.Database {
    public class Context : DbContext {
        private readonly string _connectionString;

        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<Role> Roles { get; set; }

        public Context() : this("Data Source=./apiKeys.sqlite") {
        }

        public Context(string connectionString) {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(this._connectionString);
        }
    }
}
