namespace CourseBookingAppBackend.src.Application.Commands.Users;

public sealed record DeleteUserCommand(
    int TargetUserId,
    int AdminUserId
);
