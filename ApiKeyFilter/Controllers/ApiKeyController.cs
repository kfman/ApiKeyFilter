using System;
using System.Linq;
using ApiKeyFilter.Database;
using ApiKeyFilter.Database.Interfaces;
using ApiKeyFilter.Dto;
using ApiKeyFilter.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiKeyFilter.Controllers {
    [ApiController]
    [Route("api/accessControl/[controller]")]
    [LevelFilter(LevelFilter.MasterKeyOnly)]
    public class ApiKeyController : ControllerBase {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AutoMapper.Mapper _mapper;

        public ApiKeyController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
            _mapper = new AutoMapper.Mapper(new MapperConfiguration(cfg => {
                cfg.CreateMap<ApiKey, ApiKeyDto>().ForMember(d => d.Roles,
                    opt => opt.MapFrom(m => m.Roles.Select(r => r.Role).ToList()));
                cfg.CreateMap<Role, RoleDto>().ReverseMap();
            }));
        }

        [HttpGet]
        public IActionResult Get()
            => Ok(_unitOfWork.ApiKeys.Get().Select(m => _mapper.Map<ApiKeyDto>(m)).ToList());

        [HttpGet("{id}")]
        public IActionResult Get(string id)
            => Ok(_mapper.Map<ApiKeyDto>(_unitOfWork.ApiKeys.Get(id)));


        [HttpPost]
        public IActionResult AddNew([FromBody] ApiKeyDto apikey) {
            var apiKey = new ApiKey(apikey.Description);
            _unitOfWork.ApiKeys.Add(apiKey);
            return Ok(_mapper.Map<ApiKeyDto>(apiKey));
        }

        [HttpDelete("{apiKey}")]
        public IActionResult DeleteApiKey(string apiKey) {
            _unitOfWork.ApiKeys.Delete(apiKey, false);
            return Ok();
        }

        [HttpDelete("{apiKey}/role/{role}")]
        public IActionResult DeleteRoleFromApiKey(string apiKey, string role) {
            _unitOfWork.Mediator.RemoveApiKeyFromRole(apiKey, role);
            return Ok();
        }

        [HttpPost("{apiKey}/role/{role}")]
        public IActionResult AddRoleToApiKey(string apiKey, string role) {
            _unitOfWork.Mediator.AddApiKeyToRole(apiKey, role);
            return Ok();
        }
    }
}
