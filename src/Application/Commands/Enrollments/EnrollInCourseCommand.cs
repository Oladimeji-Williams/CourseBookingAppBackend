namespace CourseBookingAppBackend.src.Application.Commands.Enrollments;
public sealed record EnrollInCourseCommand(
    int UserId,
    int CourseId
);
