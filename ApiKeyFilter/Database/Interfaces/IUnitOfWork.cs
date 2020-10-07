using ApiKeyFilter.Models;

namespace ApiKeyFilter.Database.Interfaces {
    public interface IUnitOfWork {
        public IApiKeyRepository ApiKeys { get; set; }
        public IRepository<Role> Roles { get; set; }
        void SaveChanges();
    }
}