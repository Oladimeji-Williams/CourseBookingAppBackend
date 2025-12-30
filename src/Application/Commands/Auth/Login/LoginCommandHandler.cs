using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Application.Abstractions.Security;
using CourseBookingAppBackend.src.Application.DTOs;
using CourseBookingAppBackend.src.Application.Mappers;

namespace CourseBookingAppBackend.src.Application.Commands.Auth.Login;

public sealed class LoginCommandHandler
{
    private readonly IAuthRepository _repo;
    private readonly IPasswordService _passwords;
    private readonly ITokenService _tokens;

    public LoginCommandHandler(IAuthRepository repo, IPasswordService passwords, ITokenService tokens)
    {
        _repo = repo;
        _passwords = passwords;
        _tokens = tokens;
    }
    public async Task<AuthResponseDto> Handle(LoginCommand command)
    {
        var email = command.Email.Trim().ToLowerInvariant();
        var user = await _repo.GetByEmailAsync(email)
            ?? throw new UnauthorizedAccessException("Invalid email or password");

        if (!_passwords.Verify(user.PasswordHash, command.Password))
            throw new UnauthorizedAccessException("Invalid email or password");

        if (!user.IsEmailConfirmed)
            throw new UnauthorizedAccessException("Please confirm your email");

        return user.ToAuthResponse(_tokens.Generate(user));
    }
}


