using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using FluentValidation;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Validators;

public class UpdateUserDtoValidator: AbstractValidator<UpdateUserDto>
{
  public UpdateUserDtoValidator()
  {
    RuleFor(x => x.FirstName)
      .MaximumLength(100)
      .When(x => x.FirstName != null);
    RuleFor(x => x.LastName)
      .MaximumLength(100)
      .When(x => x.LastName != null);
    RuleFor(x => x.PhoneNumber)
      .Matches(@"^\+?\d{7,15}$")
      .When(x => x.PhoneNumber != null)
      .WithMessage("Phone number must be numeric and 7–15 digits long.");
    RuleFor(x => x.PhysicalAddress)
      .MaximumLength(300)
      .When(x => x.PhysicalAddress != null);
  }
}

public class ChangeUserRoleDtoValidator : AbstractValidator<ChangeRoleDto>
{
  public ChangeUserRoleDtoValidator()
  {
    RuleFor(x => x.Type)
      .IsInEnum()
      .WithMessage("Invalid user role");
  }
}
