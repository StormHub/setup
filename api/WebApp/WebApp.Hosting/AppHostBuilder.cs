using System.Text.Json.Serialization;
using App.Shared;
using App.Shared.Messaging;
using Microsoft.AspNetCore.Mvc;
using WebApp.Hosting.GetWeatherForecast;

namespace WebApp.Hosting;

internal static class AppHostBuilder
{
    public static IHost Build(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.ConfigureHttpJsonOptions(
            options =>
            {
                options.SerializerOptions.WriteIndented = true;
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });
        builder.Services.AddMediator(typeof(Program).Assembly);

        var app = builder.Build();
        
        app.MapGet("/weatherforecast",
            async ([AsParameters] GetWeatherForecastQuery query, [FromServices] IMediator mediator, CancellationToken token) =>
                await mediator.Query(query, token));

        return app;
    }
}