using App.Shared.Messaging;

namespace WebApp.Hosting.GetWeatherForecast;

public sealed class GetWeatherForecastQueryResponse : IQueryResponse
{
    public required string Location { get; init; }
    
    public required decimal Temperature { get; init; }
    
    public required string FeelsLike { get; init; }
}