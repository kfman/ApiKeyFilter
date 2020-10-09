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
            var apiKey = new ApiKey {
                Description = apikey.Description,
                Id = Guid.NewGuid().ToString()
            };
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
