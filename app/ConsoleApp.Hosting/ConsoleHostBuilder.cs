using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleApp.Hosting;

internal static class ConsoleHostBuilder
{
    public static IHost Build(string[] args) => Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddJsonFile("appsettings.json", false);
                builder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true);

                if (context.HostingEnvironment.IsDevelopment())
                {
                    builder.AddUserSecrets<Program>();
                }

                builder.AddEnvironmentVariables();
            })
        .ConfigureServices((_, services) =>
        {
            services.AddTransient<TextParser>();
        })
        .Build();
}