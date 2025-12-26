using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Queries.Users;

public sealed class GetUsersQueryHandler
{
    private readonly IUserRepository _users;

    public GetUsersQueryHandler(IUserRepository users)
    {
        _users = users;
    }

    public async Task<IEnumerable<UserDto>> Handle()
    {
        var users = await _users.GetUsersAsync();
        return users.Select(UserMapper.ToDto);
    }
}
