using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Courses;

public sealed record UpdateCourseCommand(
    int CourseId,
    UpdateCourseDto Dto
);
