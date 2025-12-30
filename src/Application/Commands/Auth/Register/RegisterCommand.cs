namespace CourseBookingAppBackend.src.Application.Commands.Auth.Register;
public sealed record RegisterCommand(
    string Email,
    string Password
);
