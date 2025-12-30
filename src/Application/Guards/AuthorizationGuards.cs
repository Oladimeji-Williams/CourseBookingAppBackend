using CourseBookingAppBackend.src.Domain.Entities;
using CourseBookingAppBackend.src.Domain.Enums;

namespace CourseBookingAppBackend.src.Application.Guards;

internal static class AuthorizationGuards
{
    public static void EnsureAdmin(User user)
    {
        if (user.Type != UserType.Admin)
            throw new UnauthorizedAccessException();
    }
}
