using ApiKeyFilter.Controllers;
using ApiKeyFilter.Database;
using ApiKeyFilter.Database.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApiKeyFilter.Extensions {
    public static class ServiceExtension {
        public static void AddApiKeyController(this IServiceCollection service, string masterApiKey,
            bool logAccess = false) {
            UnitOfWork.MasterApiKey = masterApiKey;
            service.AddMvc().AddApplicationPart(typeof(ApiKeyController).Assembly);
            service.AddTransient<IUnitOfWork>(_ =>
                new UnitOfWork("Data Source=apiKeys.sqlite", logAccess));
        }
    }
}
