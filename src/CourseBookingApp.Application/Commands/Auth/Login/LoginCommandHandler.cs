using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Security;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Auth.Login;

public sealed class LoginCommandHandler(
    IAuthRepository repo,
    IPasswordService passwords,
    ITokenService tokens)
{
    private readonly IAuthRepository _repo = repo;
    private readonly IPasswordService _passwords = passwords;
    private readonly ITokenService _tokens = tokens;

  public async Task<AuthResponseDto?> Handle(LoginCommand command)
    {
        var email = command.Email.Trim().ToLowerInvariant();
        var user = await _repo.GetByEmailAsync(email);

        if (user is null) return null;
        if (!_passwords.Verify(user.PasswordHash, command.Password))
            return null;

        return user.ToAuthResponse(_tokens.Generate(user));
    }
}
