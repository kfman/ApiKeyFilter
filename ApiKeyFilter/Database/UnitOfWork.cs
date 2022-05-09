using System.Text.RegularExpressions;
using ApiKeyFilter.Database.Interfaces;
using ApiKeyFilter.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiKeyFilter.Database {
    public class UnitOfWork : IUnitOfWork {
        private readonly bool _logAccess;
        private readonly Context _context;
        public IRepository<ApiKey> ApiKeys { get; set; } = null!;
        public IRepository<Role> Roles { get; set; } = null!;
        public IRepository<LogEntry> LogEntries { get; set; } = null!;
        public static string MasterApiKey { get; set; } = "00000000-0000-0000-0000-000000000000";

        public IMediator Mediator { get; set; }

        public void SaveChanges() {
            _context.SaveChanges();
        }

        public string GetDatabasePath() {
            var regex = new Regex("^(?<DataSource>[Dd]ata [Ss]ource=)(?<FileName>.*)$");
            var matches = regex.Matches(_context.Database?.GetConnectionString() ?? "");
            return matches.Count == 0 ? "" : matches[0].Groups["FileName"].Value;
        }

        public UnitOfWork(string connectionString, bool logAccess) {
            _logAccess = logAccess;
            _context = new Context(connectionString);
            Mediator = new Mediator(this);
            InitRepositories();
        }

        private void InitRepositories() {
            ApiKeys = new Repository<ApiKey>(_context.ApiKeys, _context,
                (db) => db.Include(a => a.Roles).ThenInclude(r => r.Role),
                (db) => db.Include(a => a.Roles).ThenInclude(r => r.Role));
            Roles = new Repository<Role>(_context.Roles, _context,
                getIncludeSingle: (db) => db.Include(r => r.ApiKeys));
            LogEntries = new Repository<LogEntry>(_context.LogEntries, _context);
        }
    }
}
