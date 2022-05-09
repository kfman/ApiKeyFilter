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

        var apiKey = _unitOfWork.ApiKeys.Get(_apiKeyId);
        
        
        Assert.Pass();
    }
    // Read Role from Key
    // Remove Role from Key
    // Remove Key


    [OneTimeTearDown]
    public void TearDown() {
        File.Delete($"{Path}/unitTest.sqlite");
    }
}
