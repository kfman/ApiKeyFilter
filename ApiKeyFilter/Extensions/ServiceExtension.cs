using ApiKeyFilter.Database;
using ApiKeyFilter.Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ApiKeyFilter.Extensions {
    public static class ServiceExtension {
        public static void AddApiKeyController(this IServiceCollection service) {
            service.AddMvc().AddApplicationPart(typeof(Controllers.ApiKeyController).Assembly);
            service.AddTransient<IUnitOfWork>(_=>new UnitOfWork("Data Source=apiKeys.sqlite"));
        }
    }
}
