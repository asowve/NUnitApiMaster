using FluentAssertions;
using LearningNUnit.BackEnd.Entities;

namespace ApiTestsWithNUnit.Common;

public class CheckBodyResponse
{
    public void CheckBodyResponseNasaApi(NasaApiEntity response)
    {
        response.Date.Should().NotBeNullOrEmpty();
        response.Explanation.Should().NotBeNullOrEmpty();
        response.Media_type.Should().NotBeNullOrEmpty();
        response.Service_version.Should().NotBeNullOrEmpty();
        response.Title.Should().NotBeNullOrEmpty();
        response.Url.Should().NotBeNullOrEmpty();
    }

}