using System.Net;

namespace WebApp.Integration.Tests.GetWeatherForecast;

public sealed class WithoutLocation(WebAppFixture fixture) : IClassFixture<WebAppFixture>
{
    [Fact]
    public async Task IsRejected()
    {
        var response = await fixture.HttpClient.GetAsync("/weatherforecast");
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}