using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Application.DTOs;
using CourseBookingAppBackend.src.Application.Mappers;
using CourseBookingAppBackend.src.Domain.Enums;

namespace CourseBookingAppBackend.src.Application.Queries.Users;

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
