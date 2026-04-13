using Jantee.BuildingBlocks.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Jantee.Modules.Users.Presentation.Endpoints.SignUp;

public record Request(string Email, string Password);
public record Response(string Username, string? Fullname, DateTime CreatedAt);

public class SignUpEndpoint : IMinimalEndpoint
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapPost($"{PresentationConstants.BaseApiEndpoint}/sign-up", (Request request) =>
        {
            var name = request.Email.Split('@')[0];
            var response = new Response(name, name, DateTime.UtcNow);
            return Results.Ok(new { message = "Register user succeeded", data = response });
        })
        .WithName("SignUp");

        return builder;
    }
}
