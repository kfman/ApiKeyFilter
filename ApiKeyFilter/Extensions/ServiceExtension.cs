using System;
using ApiKeyFilter.Controllers;
using ApiKeyFilter.Database;
using ApiKeyFilter.Database.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApiKeyFilter.Extensions {
    public static class ServiceExtension {
        public static void AddApiKeyController(this IServiceCollection service, string masterApiKey,
            bool logAccess = false, string? connectionString = null) {
            UnitOfWork.MasterApiKey = masterApiKey;
            service.AddMvc().AddApplicationPart(typeof(ApiKeyController).Assembly);
            if (!System.IO.Directory.Exists("database"))
                System.IO.Directory.CreateDirectory("database");
            service.AddTransient<IUnitOfWork>(_ =>
                new UnitOfWork(connectionString ?? "Data Source=./database/apiKeys.sqlite",
                    logAccess));
        }
    }
}
