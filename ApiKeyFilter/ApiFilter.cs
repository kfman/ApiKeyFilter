using System;
using System.Reflection;
using System.Threading.Tasks;
using ApiKeyFilter.Database;
using ApiKeyFilter.Database.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiKeyFilter {
    public class ApiFilter : IAsyncActionFilter {
        private readonly IUnitOfWork _unitOfWork;

        public ApiFilter(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        
        public Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next) {
            var actionFilter = context.ActionDescriptor.AttributeRouteInfo;
            // if (actionFilter != null)
            //     Console.WriteLine($"Action: level{actionFilter.Level}");

            var levelFilter = context.Controller.GetType().GetCustomAttribute<LevelFilter>();
            if (levelFilter != null)
                Console.WriteLine($"Controller: controller level {levelFilter.Level}");

            Console.WriteLine("ApiFilter called");

            if (!context.HttpContext.Request.Headers.ContainsKey("ApiKey")) {
                if (levelFilter == null) return next.Invoke();
                context.Result = new UnauthorizedObjectResult("ApiKey is required");
                return Task.CompletedTask;
            }

            var apiKeyString = context.HttpContext.Request.Headers["ApiKey"].ToString();
            var apiKey = _unitOfWork.ApiKeys.Get(apiKeyString);
            
            return next.Invoke();
        }
    }
}
