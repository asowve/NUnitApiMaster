using System.Net;
using System.Text.Json;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using ApiTestsWithNUnit.Common;
using ApiTestsWithNUnit.Config;
using ApiTestsWithNUnit.Entities;
using FluentAssertions;
using LearningNUnit.BackEnd.Entities;
using NUnit.Framework;
using RestSharp.Serializers;

namespace ApiTestsWithNUnit.Tests;

[TestFixture]
[AllureNUnit]
[AllureFeature("Nasa API Tests")]
public class NasaApiTest
{
    private Common.Requests _api;
    private Dictionary<string, string> _headers;
    private Config.Config _config;
    private string _api_key;
    private CheckBodyResponse CheckBodyResponse = new();
    
    [SetUp]
    [AllureBefore("Loading the test variables")]
    public void Setup()
    {
        _headers  = new Dictionary<string, string>
        {
            { "Content-Type", "application/json" }
        };
        _config = TestConfig.GetConfig();
        _api = new Common.Requests(_config.NasaBaseUrl);
        _api_key = $"api_key={_config.ApiToken}";
    }
    
    [Test]
    [AllureOwner("@m4rri4nne")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureDescription("Search for APOD")]
    public async Task SearchApodSucess()
    {
        var response = await _api.GetAsync<string>($"/planetary/apod?{_api_key}", _headers);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        if (response.Content != null)
        {
            var data = JsonSerializer.Deserialize<NasaApiEntity>(
                response.Content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );
            CheckBodyResponse.CheckBodyResponseNasaApi(data);
        }
    }
    [Test]
    [AllureOwner("@m4rri4nne")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureDescription("Search for with filter date")]
    public async Task SearchApodWithDate()
    {
            var queryParameters = "date=2023-05-01";
            var response = await _api.GetAsync<string>($"/planetary/apod?{_api_key}&{queryParameters}", _headers);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            if (response.Content != null)
            {
                var data = JsonSerializer.Deserialize<NasaApiEntity>(
                    response.Content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
                CheckBodyResponse.CheckBodyResponseNasaApi(data);
            }
    }


    [Test]
    [AllureOwner("@m4rri4nne")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureDescription("Search for with filter date")]
    public async Task SearchApodWithDateWrongFormat()
    {
        var queryParameters = "date=2023/05/01";
        var response = await _api.GetAsync<string>($"/planetary/apod?{_api_key}&{queryParameters}", _headers);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Test]
    [AllureOwner("@m4rri4nne")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureDescription("Search for with filter date")]
    public async Task SearchApodWithStartDateEndDate()
    {
        const string startDate = "2023-05-01";
        const string endDate = "2023-06-01";
        var queryParameters = $"start_date={startDate}&end_date={endDate}";
        var response = await _api.GetAsync<string>($"/planetary/apod?{_api_key}&{queryParameters}", _headers);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
        
    [Test]
    [AllureOwner("@m4rri4nne")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureDescription("Search for with filter date - Start Date bigger than End Date")]
    public async Task SearchApodWithStartDateBiggerThanEndDate()
    {
        const string startDate = "2023-12-01";
        const string endDate = "2023-11-01";
        var queryParameters = $"start_date={startDate}&end_date={endDate}";
        var response = await _api.GetAsync<string>($"/planetary/apod?{_api_key}&{queryParameters}", _headers);
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    [Test]
    [AllureOwner("@m4rri4nne")]
    [AllureSeverity(SeverityLevel.critical)]
    [AllureDescription("Search for with invalid token")]
    public async Task SearchApodWithInvalidToken()
    {
        var token = "invalidToken";
        var response = await _api.GetAsync<string>($"/planetary/apod?{token}", _headers);
        response.StatusCode.Should().Be(HttpStatusCode.Forbidden);
    }
}