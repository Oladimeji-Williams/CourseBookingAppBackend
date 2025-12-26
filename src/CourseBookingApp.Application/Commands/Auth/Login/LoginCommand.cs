namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Auth.Login;


public sealed record LoginCommand(
    string Email,
    string Password
);
