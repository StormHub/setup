using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Simulator;
using Simulator.Instructions;

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
            services.AddTransient(provider => 
                new InputParser(Console.Out, provider.GetRequiredService<ILoggerFactory>()));
            services.AddTransient<RobotSimulator>();
        })
        .Build();
    
    await host.StartAsync();
    var lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();

    await using var scope = host.Services.CreateAsyncScope();
    var simulator = scope.ServiceProvider.GetRequiredService<RobotSimulator>();
    
    Console.WriteLine(
        """
        Toy Robot Simulator
        ==================
        
        Commands:
        - PLACE X,Y,DIRECTION
          Place the robot on the table at coordinates (X,Y) facing DIRECTION (NORTH, SOUTH, EAST, WEST).
          If the robot is already on the table, DIRECTION can be omitted to keep the current facing direction.
          Note parameters are separated by commas *without spaces*.
        
        - MOVE: Move the robot one unit forward in the direction it is currently facing.
        
        - LEFT: 
          Rotate the robot 90 degrees to the left without changing its position.
          
        - RIGHT: 
          Rotate the robot 90 degrees to the right without changing its position.
          
        - REPORT: 
          Output the current position and direction of the robot in the format "X,Y,DIRECTION".
          Note if the robot is not on the table, output is empty.
          
        """);
    simulator.Run(Console.In, lifetime.ApplicationStopping);
    Console.WriteLine("Goodbye!");

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