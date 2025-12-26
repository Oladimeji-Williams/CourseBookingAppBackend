using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Enums;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Queries.Users;

public sealed class GetUserByIdQueryHandler
{
    private readonly IUserRepository _users;

    public GetUserByIdQueryHandler(IUserRepository users)
    {
        _users = users;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery query)
    {
        if (query.RequestingUserRole != UserType.Admin
            && query.RequestingUserId != query.TargetUserId)
            throw new UnauthorizedAccessException();

        var user = await _users.GetUserByIdAsync(query.TargetUserId)
            ?? throw new KeyNotFoundException();

        return user.ToDto();
    }
}
