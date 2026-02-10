using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Simulator;
using Simulator.Instructions;
using Simulator.Robots;

IHost? host = default;
try
{
    host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.AddJsonFile("appsettings.json", false);
            builder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true);
        })
        .ConfigureServices((_, services) =>
        {
            services.AddTransient<RobotSimulator>();
            services.AddTransient<InputParser>();
            services.AddTransient<ConsoleSimulator>();
        })
        .Build();
    
    await host.StartAsync();
    var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();

    await using var scope = host.Services.CreateAsyncScope();
    var consoleSimulator = scope.ServiceProvider.GetRequiredService<ConsoleSimulator>();
    consoleSimulator.Run(lifetime.ApplicationStopping);
    
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