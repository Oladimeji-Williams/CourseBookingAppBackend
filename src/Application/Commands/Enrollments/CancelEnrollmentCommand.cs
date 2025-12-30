namespace CourseBookingAppBackend.src.Application.Commands.Enrollments;

public sealed record CancelEnrollmentCommand(
    int UserId,
    int CourseId
);
