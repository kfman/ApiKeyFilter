using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ApiKeyFilter.Extensions {
    public static class ServiceExtension {
        public static void AddApiKeyController(this IMvcBuilder builder) =>
            builder.SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddApplicationPart(typeof(Controllers.ApiKeyController).Assembly);
    }
}
