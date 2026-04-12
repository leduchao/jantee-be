using Jantee.BuildingBlocks.Web;
using Microsoft.AspNetCore.Builder;

namespace Jantee.Modules.Users.Presentation.Extensions;

public static class UsersPresentationExtensions
{
    public static WebApplicationBuilder AddUsersPresentation(this WebApplicationBuilder builder)
    {
        builder.AddMinimalEndpoints(assemblies: typeof(UsersPresentationRoot).Assembly);
        return builder;
    }
}
