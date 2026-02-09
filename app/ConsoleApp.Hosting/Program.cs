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
    var parser = scope.ServiceProvider.GetRequiredService<TextParser>();
    await parser.Parse(
        """
        [[ ## next_thought ## ]]
        To solve the problem, I need to first calculate the product of 12 and 15. After obtaining this result, I will add 7 to it. Therefore, my initial step is to use the Multiply tool with the numbers 12 and 15.
                   
        [[ ## next_tool_name ## ]]
        Multiply
                   
        [[ ## next_tool_args ## ]]
        { "number1": 12, "number2": 15 }
                   
        [[ ## completed ## ]]
        """,
        lifetime.ApplicationStopping);
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