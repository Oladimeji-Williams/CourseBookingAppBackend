using CourseBookingAppBackend.src.Application.DTOs;

namespace CourseBookingAppBackend.src.Application.Commands.Courses;

public sealed record UpdateCourseCommand(
    int CourseId,
    UpdateCourseDto Dto
);
