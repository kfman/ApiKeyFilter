using ApiKeyFilter.Models;

namespace ApiKeyFilter.Database.Interfaces {
    public interface IUnitOfWork {
        public IApiKeyRepository ApiKeys { get; set; }
        public IRepository<Role> Roles { get; set; }
        public IRepository<LogEntry> LogEntries { get; set; }
        void SaveChanges();
    }
}