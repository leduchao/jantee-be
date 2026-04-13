namespace Jantee.Modules.Users.Domain.Entities;

public class User
{
    public uint Id { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
}