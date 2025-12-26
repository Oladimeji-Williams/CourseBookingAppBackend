using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Guards;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Users;

public sealed class ChangeUserRoleCommandHandler
{
    private readonly IUserRepository _users;

    public ChangeUserRoleCommandHandler(IUserRepository users)
    {
        _users = users;
    }

    public async Task<UserDto> Handle(ChangeUserRoleCommand command)
    {
        var admin = await _users.GetUserByIdAsync(command.AdminUserId)
            ?? throw new UnauthorizedAccessException();

        AuthorizationGuards.EnsureAdmin(admin);

    var user = await _users.GetUserByIdAsync(command.TargetUserId)
            ?? throw new KeyNotFoundException();

        user.ChangeRole(command.NewRole);
        await _users.SaveChangesAsync();

        return user.ToDto();
    }
}
