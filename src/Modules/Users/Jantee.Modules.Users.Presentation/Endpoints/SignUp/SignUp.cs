using Jantee.BuildingBlocks.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Jantee.Modules.Users.Presentation.Endpoints.SignUp;

public record SignUpRequest(string Email, string Password);

public class SignUpEndpoint : IMinimalEndpoint
{
    public IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/sign-up", () =>
        {
            return Results.Ok("Register user succeeded");
        })
        .WithName("SignUp");

        return builder;
    }
}
