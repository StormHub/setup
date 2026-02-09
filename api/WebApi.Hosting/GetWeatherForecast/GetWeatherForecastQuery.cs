using App.Shared.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Hosting.GetWeatherForecast;

public sealed class GetWeatherForecastQuery : IQuery<GetWeatherForecastQueryResponse>
{
    [FromQuery]
    public required string Location { get; init; }
}