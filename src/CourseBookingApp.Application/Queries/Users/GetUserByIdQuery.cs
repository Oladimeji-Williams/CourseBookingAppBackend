using CourseBookingAppBackend.src.CourseBookingApp.Domain.Enums;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Queries.Users;

public sealed record GetUserByIdQuery(
    int TargetUserId,
    int RequestingUserId,
    UserType RequestingUserRole
);
