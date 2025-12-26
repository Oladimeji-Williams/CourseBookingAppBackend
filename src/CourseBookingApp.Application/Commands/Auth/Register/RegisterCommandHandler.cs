using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Security;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Auth.Register;

public sealed class RegisterCommandHandler
{
    private readonly IAuthRepository _repo;
    private readonly IPasswordService _passwords;
    private readonly ITokenService _tokens;

    public RegisterCommandHandler(
        IAuthRepository repo,
        IPasswordService passwords,
        ITokenService tokens)
    {
        _repo = repo;
        _passwords = passwords;
        _tokens = tokens;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand command)
    {
        var email = command.Email.Trim().ToLower();

        if (await _repo.GetByEmailAsync(email) != null)
            throw new InvalidOperationException("User with this email already exists");

        var hash = _passwords.Hash(command.Password);
        var user = User.Create(email, hash);

        await _repo.AddAsync(user);
        await _repo.SaveChangesAsync();

        return user.ToAuthResponse(_tokens.Generate(user));
    }
}
