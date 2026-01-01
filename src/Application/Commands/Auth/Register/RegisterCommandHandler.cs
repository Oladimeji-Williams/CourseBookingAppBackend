using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Application.Abstractions.Security;
using CourseBookingAppBackend.src.Application.DTOs;
using CourseBookingAppBackend.src.Application.Mappers;
using CourseBookingAppBackend.src.Domain.Entities;
using CourseBookingAppBackend.src.Application.Abstractions.Email;
using Microsoft.Extensions.Configuration;

namespace CourseBookingAppBackend.src.Application.Commands.Auth.Register;

public sealed class RegisterCommandHandler
{
    private readonly IAuthRepository _repo;
    private readonly IPasswordService _passwords;
    private readonly ITokenService _tokens;
    private readonly IEmailService _email;
    private readonly string _frontendBaseUrl;

    public RegisterCommandHandler(
        IAuthRepository repo,
        IPasswordService passwords,
        ITokenService tokens,
        IEmailService email,
        IConfiguration configuration) // Inject configuration
    {
        _repo = repo;
        _passwords = passwords;
        _tokens = tokens;
        _email = email;

        // Try to read FrontendBaseUrl from appsettings
        _frontendBaseUrl = configuration["FrontendBaseUrl"]
            ?? "http://localhost:4200"; // fallback for development
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand command)
    {
        var email = command.Email.Trim().ToLowerInvariant();

        if (await _repo.GetByEmailAsync(email) != null)
            throw new InvalidOperationException("User already exists");

        // Hash password and create user
        var hash = _passwords.Hash(command.Password);
        var user = User.Create(email, hash);

        // Generate email confirmation token
        var token = Guid.NewGuid().ToString("N");
        user.SetEmailConfirmationToken(token);

        await _repo.AddAsync(user);
        await _repo.SaveChangesAsync();

        // Build confirmation link dynamically based on environment
        var link =
            $"{_frontendBaseUrl}/(overlay:confirm-email)?email={email}&token={token}";

        // Send email
        await _email.SendAsync(
            email,
            "Confirm your email",
            $"<p>Please confirm your email <a href='{link}'>here</a>.</p>"
        );

        return user.ToAuthResponse(_tokens.Generate(user));
    }
}
