using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Enums;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Users;

public sealed class UpdateUserCommandHandler
{
    private readonly IUserRepository _users;

    public UpdateUserCommandHandler(IUserRepository users)
    {
        _users = users;
    }

    public async Task<UserDto> Handle(UpdateUserCommand command)
    {
        if (command.CurrentUserRole != UserType.Admin &&
            command.TargetUserId != command.CurrentUserId)
            throw new UnauthorizedAccessException();

        var user = await _users.GetUserByIdAsync(command.TargetUserId)
            ?? throw new KeyNotFoundException();

        user.MapUpdate(command.Dto);
        await _users.SaveChangesAsync();

        return user.ToDto();
    }
}
