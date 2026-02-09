using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] assemblies)
    {
        // Register commands and queries in the assemblies as transient services.
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes()
                .Where(t => t is { IsAbstract: false, IsInterface: false });

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                                (i.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                                 i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)));

                foreach (var @interface in interfaces)
                {
                    services.TryAddTransient(@interface, type);
                }
            }
        }
        
        services.AddScoped<IMediator>(provider => new Mediator(provider));
        return services;
    }
}