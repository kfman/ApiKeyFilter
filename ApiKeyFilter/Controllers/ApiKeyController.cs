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
    }
}
