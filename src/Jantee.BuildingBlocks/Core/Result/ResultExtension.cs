using Microsoft.AspNetCore.Http;

namespace Jantee.BuildingBlocks.Core.Result;

public static class ResultExtension
{
    public static IResult ToHttpResult<T>(this Result<T> result)
    {
        return result.IsSuccess ? Results.Ok(result) : Results.BadRequest(result);
    }
}
