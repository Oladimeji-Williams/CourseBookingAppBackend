using CourseBookingAppBackend.src.Domain.Enums;

namespace CourseBookingAppBackend.src.Application.Queries.Users;

public sealed record GetUserByIdQuery(
    int TargetUserId,
    int RequestingUserId,
    UserType RequestingUserRole
);
