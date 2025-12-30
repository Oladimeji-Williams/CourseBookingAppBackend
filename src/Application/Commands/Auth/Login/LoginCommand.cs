namespace CourseBookingAppBackend.src.Application.Commands.Auth.Login;


public sealed record LoginCommand(
    string Email,
    string Password
);
