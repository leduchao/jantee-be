using Microsoft.AspNetCore.Routing;

namespace Jantee.BuildingBlocks.Web;

public interface IMinimalEndpoint
{
    IEndpointRouteBuilder MapEndpoint(IEndpointRouteBuilder builder);
}
