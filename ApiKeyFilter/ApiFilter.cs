using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ApiKeyFilter.Database;
using ApiKeyFilter.Database.Interfaces;
using ApiKeyFilter.Models;
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

            var levelFilter = context.Controller.GetType().GetCustomAttributes<LevelFilter>()?.ToList();
            if (levelFilter.Count == 0)
                return next.Invoke();

            if (!context.HttpContext.Request.Headers.ContainsKey("ApiKey")) {
                context.Result = new UnauthorizedObjectResult("ApiKey is required");
                AddLogEntry(context, "No ApiKey provided", false);
                return Task.CompletedTask;
            }

            var apiKeyString = context.HttpContext.Request.Headers["ApiKey"].ToString();
            if (apiKeyString == UnitOfWork.MasterApiKey) {
                AddLogEntry(context, apiKeyString, true);
                return next.Invoke();
            }

            var apiKey = _unitOfWork.ApiKeys.Get(apiKeyString);
            if (apiKey == null) {
                context.Result = new UnauthorizedObjectResult("ApiKey is invalid");
                AddLogEntry(context, apiKeyString, false);
                return Task.CompletedTask;
            }
            
            if (levelFilter.Any(l=>l.Level == LevelFilter.AllKeysAllowed)){
                AddLogEntry(context, apiKeyString, true);
                return next.Invoke();
            }

            if (!apiKey.ContainsRoll(levelFilter.Select(l => l.Level).ToList())) {
                context.Result = new UnauthorizedObjectResult("ApiKey is invalid");
                AddLogEntry(context, apiKeyString, false);
                return Task.CompletedTask;
            }

            AddLogEntry(context, apiKeyString, true);
            return next.Invoke();
        }

        private void AddLogEntry(ActionContext context, string apiKeyString, bool granted) {
            _unitOfWork.LogEntries.Add(new LogEntry {
                Id = Guid.NewGuid().ToString(),
                ApiKeyString = apiKeyString,
                Controller = context.HttpContext.Request.Path.Value,
                AccessGranted = granted
            });
        }
    }
}
