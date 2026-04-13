using Jantee.BuildingBlocks.Core.Cqrs;

namespace Jantee.Modules.Users.Application.Features.SignUp;

public record SignUpCommand(string Email, string Username, string Password);

public record SignUpResponseDto(string Email, string Username, string Password, DateTime CreatedAt);

public class SignUpHandler : ICommandHandler<SignUpCommand, SignUpResponseDto>
{
    public async Task<SignUpResponseDto> Handle(SignUpCommand command, CancellationToken ct)
    {
        var response = new SignUpResponseDto(command.Email, command.Username, command.Password, DateTime.UtcNow);
        await Task.CompletedTask;

        return response;
    }
}
