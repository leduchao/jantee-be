using Jantee.BuildingBlocks.Core.Cqrs;
using Jantee.BuildingBlocks.Web;
using Jantee.Modules.Users.Application.Features.SignUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
                var response = await dispatcher.Dispatch<SignUpCommand, SignUpResponseDto>(command, ct);

                return Results.Ok(new { message = "Register user succeeded", data = response });
            })
            .WithName("SignUp");

        return builder;
    }
}
