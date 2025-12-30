namespace CourseBookingAppBackend.src.Application.Commands.Auth.ConfirmEmail;

public sealed record ConfirmEmailCommand(string Email, string Token);
