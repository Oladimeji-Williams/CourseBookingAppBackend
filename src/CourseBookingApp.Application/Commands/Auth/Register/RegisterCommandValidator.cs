using FluentValidation;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Auth.Register;

public sealed class RegisterCommandValidator
    : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).MinimumLength(6);
    }
}
