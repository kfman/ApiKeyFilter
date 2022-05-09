using System;
using System.IO;
using ApiKeyFilter.Database;
using ApiKeyFilter.Database.Interfaces;
using ApiKeyFilter.Models;
using FluentAssertions;
using NUnit.Framework;

namespace ApiKeyFilterTest;

public class ApiKeysCurd {
    private const string Path = "./testDb";
    private IUnitOfWork _unitOfWork = null!;
    private string? _apiKeyId;

    [OneTimeSetUp]
    public void Setup() {
        Directory.CreateDirectory(Path);
    }

    [SetUp]
    public void SetUp() {
        _unitOfWork = new UnitOfWork($"Data Source={Path}/unitTest.sqlite", true);
    }

    [Test]
    [Order(10)]
    public void SetupSuccessful() {
        _unitOfWork.GetDatabasePath().Should().StartWith(Path);
    }

    [Test]
    [Order(20)]
    public void CreateApiKey() {
        var apiKey = new ApiKey("Dummy for Test");
        _apiKeyId = apiKey.Id;
        _unitOfWork.ApiKeys.Add(apiKey);
        _unitOfWork.SaveChanges();
    }


    [Test]
    [Order(30)]
    public void ReadAllKeys() {
        var keys = _unitOfWork.ApiKeys.Get();
        keys.Should().Contain(k => k.Id == _apiKeyId);
    }


    [Test]
    [Order(40)]
    public void AddRoleToKey() {
        if (_apiKeyId == null)
            throw new Exception("No ApiKey Id is set. Run test in correct order!");

        _unitOfWork.Mediator.AddApiKeyToRole(_apiKeyId, "TestRole");

        Assert.Pass();
    }

    [Test]
    [Order(50)]
    public void ReadRoleFromKey_ShouldContainRole() {
        _apiKeyId.Should().NotBeNull();

        var apiKey = _unitOfWork.ApiKeys.Get(_apiKeyId);
        apiKey.Should().NotBeNull();
        apiKey?.Roles.Should().Contain(r => r.RoleId == "TestRole");
    }

    [Test]
    [Order(51)]
    public void ReadRoleFromKey_ShouldNotContainRole() {
        _apiKeyId.Should().NotBeNull();

        var apiKey = _unitOfWork.ApiKeys.Get(_apiKeyId);
        apiKey.Should().NotBeNull();
        apiKey?.Roles.Should().NotContain(r => r.RoleId == "UnknownRole");
    }

    [Test]
    [Order(60)]
    public void GetRoleWithKeys() {
        var role = _unitOfWork.Roles.Get("TestRole");
        role.Should().NotBeNull();
        role!.ApiKeys.Should().Contain(a => a.ApiKeyId == _apiKeyId);
    }

    [Test]
    [Order(70)]
    public void RemoveRoleFromKey() {
        var apiKey = _unitOfWork.ApiKeys.Get(_apiKeyId);
        _unitOfWork.Mediator.RemoveApiKeyFromRole(_apiKeyId, "TestRole");
    }

    [Test]
    [Order(71)]
    public void ReadRemovedRoleFromKey_ShouldContainRole() {
        _apiKeyId.Should().NotBeNull();

        var apiKey = _unitOfWork.ApiKeys.Get(_apiKeyId);
        apiKey.Should().NotBeNull();
        apiKey?.Roles.Should().NotContain(r => r.RoleId == "TestRole");
    }

    [Test]
    [Order(80)]
    public void RemoveKey() {
        _unitOfWork.ApiKeys.Delete(_apiKeyId);
    }

    [Test]
    [Order(81)]
    public void RemovedKeyShouldHaveDeletedDate() {
        var apiKey = _unitOfWork.ApiKeys.Get(_apiKeyId);
        apiKey.Deleted.Should().NotBeNull();
    }

    [TearDown]
    public void TearDown() {
        // Tear down UnitOfWork
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() {
        File.Delete($"{Path}/unitTest.sqlite");
    }
}
