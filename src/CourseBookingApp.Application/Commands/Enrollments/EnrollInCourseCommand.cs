namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Enrollments;
public sealed record EnrollInCourseCommand(
    int UserId,
    int CourseId
);
