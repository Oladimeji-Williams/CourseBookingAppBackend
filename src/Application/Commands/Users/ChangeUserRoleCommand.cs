using CourseBookingAppBackend.src.Domain.Enums;

namespace CourseBookingAppBackend.src.Application.Commands.Users;

public sealed record ChangeUserRoleCommand(
    int TargetUserId,
    int AdminUserId,
    UserType NewRole
);
