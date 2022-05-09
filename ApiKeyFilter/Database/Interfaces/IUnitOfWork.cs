using ApiKeyFilter.Models;

namespace ApiKeyFilter.Database.Interfaces {
    public interface IUnitOfWork {
        public IRepository<ApiKey> ApiKeys { get; set; }
        public IRepository<Role> Roles { get; set; }
        public IRepository<LogEntry> LogEntries { get; set; }
        void SaveChanges();
        string GetDatabasePath();
        public IMediator Mediator { get; set; }
    }
}
