using ApiKeyFilter.Models;

namespace ApiKeyFilter.Database {
    public interface IUnitOfWork {
        public IRepository<ApiKey> ApiKeys { get; set; }
        public IRepository<Role> Roles { get; set; }
        void SaveChanges();
    }
}