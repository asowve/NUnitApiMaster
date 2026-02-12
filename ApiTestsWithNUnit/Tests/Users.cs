using System.Net;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using ApiTestsWithNUnit.Config;
using ApiTestsWithNUnit.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace ApiTestsWithNUnit.Tests;

[TestFixture]
[AllureNUnit]
[AllureFeature("Users")]
public class Users
{
    private Common.Requests _api;
    private Dictionary<string, string> _headers;
    private Config.Config _config;
    
    [SetUp]
    [AllureBefore("Setup of client and loading the test variables")]
    public void Setup()
    {
        _headers  = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        _config = TestConfig.GetConfig();
        _api = new Common.Requests(_config.BaseUrl);
    }
    
    [Test]
    [AllureOwner("@m4rri4nne")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureDescription("Get all users")]
    public async Task GetAllUsers()
    {
        var response = await _api.GetAsync<object>("/users");
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var users = response.Content;
        users.Should().NotBeNull();
    }

    [Test]
    [AllureOwner("@m4rri4nne")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureDescription("Creation of a new user")]
    public async Task CreateNewUser()
    {
        var bodyRequest = new UsersRequestBody
        {
            firstName = _config.UserName,
            lastName = _config.LastName,
            email = _config.Email,
            age = 20
        };
        
        var response = await _api.PostAsync<object>("/users/add", bodyRequest, _headers);
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var users = response.Content;
        users.Should().Contain(bodyRequest.firstName);
        users.Should().Contain(bodyRequest.lastName);
        users.Should().Contain(bodyRequest.email);
    }

}