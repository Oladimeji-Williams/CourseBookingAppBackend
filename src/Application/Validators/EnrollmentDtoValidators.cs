

using CourseBookingAppBackend.src.Application.Commands.Enrollments;
using FluentValidation;

namespace CourseBookingAppBackend.src.Application.Validators;

public sealed class EnrollInCourseCommandValidator
    : AbstractValidator<EnrollInCourseCommand>
{
  public EnrollInCourseCommandValidator()
  {
    RuleFor(x => x.UserId).GreaterThan(0);
    RuleFor(x => x.CourseId).GreaterThan(0);
  }
}
