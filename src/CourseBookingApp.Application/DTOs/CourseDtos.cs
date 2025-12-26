using CourseBookingAppBackend.src.CourseBookingApp.Domain.Enums;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;

public sealed class CourseDto
{
  public int Id { get; init; }
  public string Title { get; init; } = null!;
  public string? Description { get; init; }
  public double Price { get; init; }
  public CourseType Type { get; init; }
  public DateTime Created { get; init; }
}
public sealed class CreateCourseDto
{
  public string Title { get; init; } = null!;
  public string? Description { get; init; }
  public double Price { get; init; }
  public CourseType Type { get; init; }
}
public sealed class UpdateCourseDto
{
  public string? Title { get; init; }
  public string? Description { get; init; }
  public double? Price { get; init; }
  public CourseType? Type { get; init; }
}
