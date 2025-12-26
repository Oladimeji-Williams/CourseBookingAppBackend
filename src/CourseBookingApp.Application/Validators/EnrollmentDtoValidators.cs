

using CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Enrollments;
using FluentValidation;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Validators;

public sealed class EnrollInCourseCommandValidator
    : AbstractValidator<EnrollInCourseCommand>
{
  public EnrollInCourseCommandValidator()
  {
    RuleFor(x => x.UserId).GreaterThan(0);
    RuleFor(x => x.CourseId).GreaterThan(0);
  }
}
