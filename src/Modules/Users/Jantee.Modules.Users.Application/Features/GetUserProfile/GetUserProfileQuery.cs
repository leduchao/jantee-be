using Jantee.BuildingBlocks.Core.Cqrs;
using Jantee.BuildingBlocks.Core.Result;
using Jantee.Modules.Users.Domain.Entities;

namespace Jantee.Modules.Users.Application.Features.GetUserProfile;

public record GetUserProfileQuery(uint UserId) : IQuery<Result<User>>;

public class GetUserProfileQueryHandler : IQueryHandler<GetUserProfileQuery, Result<User>>
{
    public async Task<Result<User>> Handle(GetUserProfileQuery query, CancellationToken ct)
    {
        var newUser = new User
        {
            Id = query.UserId,
            Username = "haole",
            Email = "haole@email.com",
            PasswordHash = "hashed-pass"
        };

        await Task.CompletedTask;

        return Result<User>.Success(newUser);
    }
}