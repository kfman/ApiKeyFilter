using ApiKeyFilter.Models;

namespace ApiKeyFilter.Database.Interfaces {
    public interface IApiKeyRepository: IRepository<ApiKey> {
    ApiKey Get(string apiKeyString);
    }
}