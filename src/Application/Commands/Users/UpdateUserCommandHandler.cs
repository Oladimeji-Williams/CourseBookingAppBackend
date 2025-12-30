using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Application.DTOs;
using CourseBookingAppBackend.src.Application.Mappers;
using CourseBookingAppBackend.src.Domain.Enums;

namespace CourseBookingAppBackend.src.Application.Commands.Users;

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
