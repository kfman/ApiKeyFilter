using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiKeyFilter {
    public class ApiFilter : IAsyncActionFilter {
        public Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next) {
            var actionFilter = context.ActionDescriptor.AttributeRouteInfo;
            // if (actionFilter != null)
            //     Console.WriteLine($"Action: level{actionFilter.Level}");

            var levelFilter = context.Controller.GetType().GetCustomAttribute<LevelFilter>();
            if (levelFilter != null)
                Console.WriteLine($"Controller: controller level {levelFilter.Level}");

            Console.WriteLine("ApiFilter called");
            return next.Invoke();
        }
    }
}
