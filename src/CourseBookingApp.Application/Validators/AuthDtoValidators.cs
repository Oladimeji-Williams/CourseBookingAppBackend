using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using FluentValidation;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Validators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{

  public RegisterDtoValidator()
  {
    RuleFor(x => x.Email).NotEmpty().EmailAddress();
    RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
  }
}

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
  public ChangePasswordDtoValidator()
  {
    RuleFor(x => x.CurrentPassword).NotEmpty();
    RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(6);
  }
}
public class LoginDtoValidator : AbstractValidator<LoginDto>
{
  public LoginDtoValidator()
  {
    RuleFor(x => x.Email).NotEmpty().EmailAddress();
    RuleFor(x => x.Password).NotEmpty();
  }
}
