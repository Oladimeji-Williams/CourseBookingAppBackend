using CourseBookingAppBackend.src.CourseBookingApp.Domain.Enums;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Users;

public sealed record ChangeUserRoleCommand(
    int TargetUserId,
    int AdminUserId,
    UserType NewRole
);
