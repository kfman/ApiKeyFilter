using System;
using ApiKeyFilter.Database.Interfaces;
using ApiKeyFilter.Models;

namespace ApiKeyFilter.Database;

public class Mediator : IMediator {
    private readonly IUnitOfWork _unitOfWork;

    public Mediator(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    public void AddApiKeyToRole(string apiKey, string role) {
        var roleEntry = _unitOfWork.Roles.Get(role) ?? _unitOfWork.Roles.Add(new Role {
            Id = role
        });

        var key = _unitOfWork.ApiKeys.Get(apiKey);
        key?.Roles.Add(new ApiKeyRoles {
            Role = roleEntry,
            ApiKeyId = apiKey
        });

        _unitOfWork.SaveChanges();
    }

    public void RemoveApiKeyFromRole(string keyId, string roleId) {
        var key = _unitOfWork.ApiKeys.Get(keyId);
        key.Roles.RemoveAll(r => r.RoleId == roleId);
        _unitOfWork.SaveChanges();
    }
}
