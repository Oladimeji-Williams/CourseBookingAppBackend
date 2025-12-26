namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Enrollments;

public sealed record CancelEnrollmentCommand(
    int UserId,
    int CourseId
);
