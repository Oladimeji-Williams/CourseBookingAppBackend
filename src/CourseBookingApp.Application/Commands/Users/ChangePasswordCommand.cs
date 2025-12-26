namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Users;
public sealed record ChangePasswordCommand(
    int UserId,
    string CurrentPassword,
    string NewPassword
);
