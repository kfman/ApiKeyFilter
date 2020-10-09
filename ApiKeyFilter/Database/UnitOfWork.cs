using ApiKeyFilter.Database.Interfaces;
using ApiKeyFilter.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiKeyFilter.Database {
    public class UnitOfWork : IUnitOfWork {
        private readonly bool _logAccess;
        private readonly Context _context;
        public IRepository<ApiKey> ApiKeys { get; set; }
        public IRepository<Role> Roles { get; set; }
        public IRepository<LogEntry> LogEntries { get; set; }
        public static string MasterApiKey { get; set; }

        public void SaveChanges() {
            _context.SaveChanges();
        }

        public UnitOfWork(string connectionString, bool logAccess) {
            _logAccess = logAccess;
            _context = new Context(connectionString);
            InitRepositories();
        }

        private void InitRepositories() {
            ApiKeys = new Repository<ApiKey>(_context.ApiKeys, _context,
                (db) => db.Include(a => a.Roles).ThenInclude(r => r.Role),
                (db) => db.Include(a => a.Roles).ThenInclude(r => r.Role));
            Roles = new Repository<Role>(_context.Roles, _context);
            LogEntries = new Repository<LogEntry>(_context.LogEntries, _context);
        }
    }
}
