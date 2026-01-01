using CourseBookingAppBackend.src.Application.Abstractions.Persistence;

namespace CourseBookingAppBackend.src.Application.Commands.Auth.ConfirmEmail;

public sealed class ConfirmEmailCommandHandler
{
    private readonly IAuthRepository _repo;

    public ConfirmEmailCommandHandler(IAuthRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(ConfirmEmailCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Token))
            throw new InvalidOperationException("Confirmation token is required");

        var token = command.Token.Trim();

        var user = await _repo.GetByEmailConfirmationTokenAsync(token)
            ?? throw new InvalidOperationException("Invalid or expired confirmation token");

        if (user.IsEmailConfirmed)
            throw new InvalidOperationException("Email is already confirmed");

        user.ConfirmEmail();
        await _repo.SaveChangesAsync();
    }
}
