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
        builder.Services.AddUsersCqrs(assemblies: typeof(UsersApplicationRoot).Assembly);
        builder.Services.AddUsersInfrastructure(builder.Configuration);
        builder.AddMinimalEndpoints(assemblies: typeof(UsersPresentationRoot).Assembly);
        
        return builder;
    }
}
