using App.Shared.Messaging;

namespace WebApi.Hosting.GetWeatherForecast;

internal sealed class GetWeatherForecastQueryHandler : IQueryHandler<GetWeatherForecastQuery, GetWeatherForecastQueryResponse>
{
    public Task<GetWeatherForecastQueryResponse> Handle(GetWeatherForecastQuery query, CancellationToken token = default)
    {
        var response = new GetWeatherForecastQueryResponse
        {
            Location = query.Location,
            Temperature = 20.5m,
            FeelsLike = "Warm"
        };

        return Task.FromResult(response);
    }
}