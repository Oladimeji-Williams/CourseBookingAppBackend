using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Domain.Enums;

namespace CourseBookingAppBackend.src.Application.Commands.Users;

public sealed class DeleteUserCommandHandler
{
    private readonly IUserRepository _users;

    public DeleteUserCommandHandler(IUserRepository users)
    {
        _users = users;
    }

    public async Task Handle(DeleteUserCommand command)
    {
        var admin = await _users.GetUserByIdAsync(command.AdminUserId)
            ?? throw new UnauthorizedAccessException();

        if (admin.Type != UserType.Admin)
            throw new UnauthorizedAccessException();

        var user = await _users.GetUserByIdAsync(command.TargetUserId)
            ?? throw new KeyNotFoundException();

        _users.Delete(user);
        await _users.SaveChangesAsync();
    }
}
