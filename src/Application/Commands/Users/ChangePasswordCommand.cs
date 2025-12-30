namespace CourseBookingAppBackend.src.Application.Commands.Users;
public sealed record ChangePasswordCommand(
    int UserId,
    string CurrentPassword,
    string NewPassword
);
