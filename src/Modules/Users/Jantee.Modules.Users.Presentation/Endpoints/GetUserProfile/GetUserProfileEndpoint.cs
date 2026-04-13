using Jantee.BuildingBlocks.Core.Cqrs;
using Jantee.BuildingBlocks.Core.Result;
using Jantee.BuildingBlocks.Web;
using Jantee.Modules.Users.Application.Features.GetUserProfile;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Jantee.Modules.Users.Presentation.Endpoints.GetUserProfile;

public class GetUserProfileEndpoint : IMinimalEndpoint
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet(
            $"{PresentationConstants.BaseApiEndpoint}/get-user-profile/{{userId}}", 
            async (uint userId, IQueryDispatcher dispatcher, CancellationToken ct = default) =>
            {
                var query = new GetUserProfileQuery(userId);
                var result = await dispatcher.Dispatch(query, ct);

                return result.ToHttpResult();
            })
            .WithName(nameof(GetUserProfile));

        return builder;
    }
}
