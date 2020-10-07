using System;
using ApiKeyFilter.Database;
using ApiKeyFilter.Database.Interfaces;
using ApiKeyFilter.Dto;
using ApiKeyFilter.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiKeyFilter.Controllers {
    [ApiController]
    [Route("api/accessControl/[controller]")]
    [LevelFilter(LevelFilter.MasterKeyOnly)]
    public class ApiKeyController : ControllerBase {
        private readonly IUnitOfWork _unitOfWork;

        public ApiKeyController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get() {
            return Ok("Hello from ApiKeyController");
        }


        [HttpPost]
        public IActionResult AddNew([FromBody] ApiKeyDto apikey) {
            var apiKey = new ApiKey {
                Description = apikey.Description,
                Id = Guid.NewGuid().ToString()
            };
            _unitOfWork.ApiKeys.Add(apiKey);
            return Ok(apiKey);
        }

        [HttpDelete("{apiKey}")]
        public IActionResult DeleteApiKey(string apiKey) {
            _unitOfWork.ApiKeys.Delete(apiKey, false);
            return Ok();
        }

        [HttpDelete("{apiKey}/role/{role}")]
        public IActionResult DeleteRoleFromApiKey(string apiKey, string role) {
            var key = _unitOfWork.ApiKeys.Get(apiKey);
            key.Roles.RemoveAll(r => r.RoleId == role);
            _unitOfWork.SaveChanges();
            return Ok();
        }

        [HttpPost("{apiKey}/role/{role}")]
        public IActionResult AddRoleToApiKey(string apiKey, string role) {
            var roleEntry = _unitOfWork.Roles.Get(role) ?? _unitOfWork.Roles.Add(new Role {
                Id = role
            });

            var key = _unitOfWork.ApiKeys.Get(apiKey);
            key.Roles.Add(new ApiKeyRoles {
                Role = roleEntry,
                ApiKeyId = apiKey
            });
            _unitOfWork.SaveChanges();

            return Ok();
        }
    }
}
