using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Enums;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;

public static class CourseMapper
{
  public static CourseDto ToDto(Course course)
  {
    return new CourseDto
    {
      Id = course.Id,
      Title = course.Title,
      Description = course.Description,
      Price = course.Price,
      Type = course.Type,
      Created = course.Created
    };
  }

  public static Course ToEntity(CreateCourseDto dto)
  {
    return new Course(
        dto.Title,
        dto.Description ?? string.Empty,
        dto.Price,
        dto.Type
    );
  }

  public static void MapUpdate(Course course, UpdateCourseDto dto)
  {
    course.UpdateDetails(
        dto.Title ?? course.Title,
        dto.Description ?? course.Description,
        dto.Price ?? course.Price,
        dto.Type ?? course.Type
    );
  }
}
