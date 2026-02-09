using ConsoleApp.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost? host = default;
try
{
    host = ConsoleHostBuilder.Build(args);

    await host.StartAsync();
    var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();

    await using var scope = host.Services.CreateAsyncScope();
    lifetime.StopApplication();

    await host.WaitForShutdownAsync(lifetime.ApplicationStopping);
}
catch (Exception ex)
{
    Console.WriteLine($"Host terminated unexpectedly! \n{ex}");
}
finally
{
    host?.Dispose();
}