namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Users;

public sealed record DeleteUserCommand(
    int TargetUserId,
    int AdminUserId
);
