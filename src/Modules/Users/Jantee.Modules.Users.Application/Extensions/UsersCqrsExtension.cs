using System.Reflection;
using Jantee.BuildingBlocks.Core.Cqrs;
using Microsoft.Extensions.DependencyInjection;

namespace Jantee.Modules.Users.Application.Extensions;

public static class UsersCqrsExtension
{
    public static IServiceCollection AddUsersCqrs(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
