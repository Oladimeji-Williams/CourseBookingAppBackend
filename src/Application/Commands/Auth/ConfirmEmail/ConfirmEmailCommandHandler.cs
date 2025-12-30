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
        var email = command.Email.Trim().ToLowerInvariant();

        var user = await _repo.GetByEmailAsync(email)
            ?? throw new InvalidOperationException("User not found");

        user.ConfirmEmail(command.Token);
        await _repo.SaveChangesAsync();
    }
}
