using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Enums;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Guards;

internal static class AuthorizationGuards
{
    public static void EnsureAdmin(User user)
    {
        if (user.Type != UserType.Admin)
            throw new UnauthorizedAccessException();
    }
}
