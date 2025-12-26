using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Security;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Users;

public sealed class ChangePasswordCommandHandler
{
    private readonly IUserRepository _users;
    private readonly IPasswordService _passwords;

    public ChangePasswordCommandHandler(
        IUserRepository users,
        IPasswordService passwords)
    {
        _users = users;
        _passwords = passwords;
    }

    public async Task Handle(ChangePasswordCommand command)
    {
        var user = await _users.GetUserByIdAsync(command.UserId)
            ?? throw new KeyNotFoundException("User not found");

        if (!_passwords.Verify(user.PasswordHash, command.CurrentPassword))
            throw new UnauthorizedAccessException("Invalid password");

        user.ChangePassword(_passwords.Hash(command.NewPassword));
        await _users.SaveChangesAsync();
    }
}
