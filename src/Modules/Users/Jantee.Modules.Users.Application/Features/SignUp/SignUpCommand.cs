using Jantee.BuildingBlocks.Core.Cqrs;
using Jantee.BuildingBlocks.Core.Result;

namespace Jantee.Modules.Users.Application.Features.SignUp;

public record SignUpCommand(string Email, string Username, string Password) : ICommand<Result<SignUpResponseDto>>;

public record SignUpResponseDto(string Email, string Username, string Password, DateTime CreatedAt);

public class SignUpCommandHandler : ICommandHandler<SignUpCommand, Result<SignUpResponseDto>>
{
    public async Task<Result<SignUpResponseDto>> Handle(SignUpCommand command, CancellationToken ct)
    {
        var response = new SignUpResponseDto(command.Email, command.Username, command.Password, DateTime.UtcNow);
        await Task.CompletedTask;

        return Result<SignUpResponseDto>.Success(response);
    }
}
