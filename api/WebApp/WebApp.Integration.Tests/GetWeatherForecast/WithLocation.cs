using System.Net;

namespace WebApp.Integration.Tests.GetWeatherForecast;

public sealed class WithLocation(WebAppFixture fixture) : IClassFixture<WebAppFixture>
{
    [Fact]
    public async Task IsOK()
    {
        var response = await fixture.HttpClient.GetAsync("/weatherforecast?location=brisbane");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        
        var json = await response.Content.ReadAsStringAsync();
        await VerifyJson(json);
    }
}