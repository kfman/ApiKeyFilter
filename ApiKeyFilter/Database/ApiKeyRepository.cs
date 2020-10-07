using System.Linq;
using ApiKeyFilter.Database.Interfaces;
using ApiKeyFilter.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiKeyFilter.Database {
    public class ApiKeyRepository : Repository<ApiKey>, IApiKeyRepository {
        public static string MasterApiKey { get; set; }

        public ApiKeyRepository(DbSet<ApiKey> dbSet, DbContext context) : base(dbSet, context,
            getIncludeSingle: (db) => db.Include(a => a.Roles).ThenInclude(r => r.Role)) {
        }


        public ApiKey Get(string apiKeyString) => DbSet.FirstOrDefault(a => a.Key == apiKeyString);
    }
}
