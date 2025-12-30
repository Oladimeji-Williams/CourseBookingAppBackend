using CourseBookingAppBackend.src.Application.DTOs;
using FluentValidation;

namespace CourseBookingAppBackend.src.Application.Validators;

public sealed class CreateCourseDtoValidator
  : AbstractValidator<CreateCourseDto>
{
  public CreateCourseDtoValidator()
  {
    RuleFor(x => x.Title)
      .NotEmpty()
      .MaximumLength(200);
    RuleFor(x => x.Description)
      .MaximumLength(2000);
    RuleFor(x => x.Price)
      .GreaterThan(0);

    RuleFor(x => x.Type)
      .IsInEnum();
  }
}

public sealed class UpdateCourseDtoValidator
  : AbstractValidator<UpdateCourseDto>
{
  public UpdateCourseDtoValidator()
  {
    RuleFor(x => x.Title)
        .MaximumLength(200)
        .When(x => x.Title != null);

    RuleFor(x => x.Description)
        .MaximumLength(2000)
        .When(x => x.Description != null);

    RuleFor(x => x.Price)
      .GreaterThan(0)
      .When(x => x.Price.HasValue);

    RuleFor(x => x.Type)
      .IsInEnum()
      .When(x => x.Type.HasValue);
  }
}