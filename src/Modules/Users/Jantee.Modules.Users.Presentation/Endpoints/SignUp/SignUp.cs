using Jantee.BuildingBlocks.Core.Cqrs;
using Jantee.BuildingBlocks.Core.Result;
using Jantee.BuildingBlocks.Web;
using Jantee.Modules.Users.Application.Features.SignUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Jantee.Modules.Users.Presentation.Endpoints.SignUp;

public record Request(string Email, string Username, string Password);

public class SignUpEndpoint : IMinimalEndpoint
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost(
            $"{PresentationConstants.BaseApiEndpoint}/sign-up", 
            async (Request request, ICommandDispatcher dispatcher, CancellationToken ct = default) =>
            {
                var command = new SignUpCommand(request.Email, request.Username, request.Password);
                var result = await dispatcher.Dispatch<SignUpCommand, Result<SignUpResponseDto>>(command, ct);

                return result.ToHttpResult();
            })
            .WithName("SignUp");

        return builder;
    }
}
