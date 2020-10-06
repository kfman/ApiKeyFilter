using ApiKeyFilter.Models;

namespace ApiKeyFilter.Database {
    public class UnitOfWork: IUnitOfWork {
        private Context _context;
        public IRepository<ApiKey> ApiKeys { get; set; }
        public IRepository<Role> Roles { get; set; }
        public void SaveChanges() {
            _context.SaveChanges();
        }

        public UnitOfWork(string connectionString) {
            _context = new Context(connectionString);
        }
    }
}