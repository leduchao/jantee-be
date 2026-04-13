using System.Reflection;
using Jantee.BuildingBlocks.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Jantee.BuildingBlocks.Web;

public static class MinimalApiExtensions
{
    /// <summary>
    /// Add minmal endpoints.
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <param name="lifetime"></param>
    /// <param name="assemblies"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddMinimalEndpoints(
        this WebApplicationBuilder applicationBuilder,
        ServiceLifetime lifetime = ServiceLifetime.Scoped,
        params Assembly[] assemblies)
    {
        var scanAssemblies = assemblies.Length > 0
            ? assemblies
            : TypeProvider.GetReferencedAssemblies(Assembly.GetCallingAssembly())
                .Concat(TypeProvider.GetApplicationPartAssemblies(Assembly.GetCallingAssembly()))
                .Distinct()
                .ToArray();

        applicationBuilder.Services.Scan(scan => scan
            .FromAssemblies(scanAssemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IMinimalEndpoint)))
            .UsingRegistrationStrategy(RegistrationStrategy.Append)
            .As<IMinimalEndpoint>()
            .WithLifetime(lifetime));

        return applicationBuilder.Services;
    }

    /// <summary>
    /// Map Minimal Endpoints.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns>IEndpointRouteBuilder.</returns>
    public static IEndpointRouteBuilder MapMinimalEndpoints(this IEndpointRouteBuilder builder)
    {
        var scope = builder.ServiceProvider.CreateScope();
        var endpoints = scope.ServiceProvider.GetServices<IMinimalEndpoint>();

        Console.WriteLine($"Found endpoints: {endpoints.Count()}");

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(builder);
        }

        return builder;
    }
}
