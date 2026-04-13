using Jantee.BuildingBlocks.Web;
using Jantee.Modules.Users.Application;
using Jantee.Modules.Users.Application.Extensions;
using Jantee.Modules.Users.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Jantee.Modules.Users.Presentation.Extensions;

public static class UsersPresentationExtension
{
    public static WebApplicationBuilder AddUsersModule(this WebApplicationBuilder builder)
    {
        var asssembly = typeof(UsersApplicationRoot).Assembly;

        builder.Services.AddUsersCqrs(assemblies: asssembly);
        builder.Services.AddUsersInfrastructure(builder.Configuration);
        builder.AddMinimalEndpoints(assemblies: asssembly);
        
        return builder;
    }
}
