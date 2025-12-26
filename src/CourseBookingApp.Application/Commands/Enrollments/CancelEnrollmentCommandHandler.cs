
using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Enrollments;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Queries.Enrollments;
public sealed class CancelEnrollmentCommandHandler
{
    private readonly IEnrollmentRepository _enrollments;

    public CancelEnrollmentCommandHandler(IEnrollmentRepository enrollments)
    {
        _enrollments = enrollments;
    }

    public async Task Handle(CancelEnrollmentCommand command)
    {
        var enrollment = await _enrollments
            .GetEnrollmentAsync(command.UserId, command.CourseId)
            ?? throw new KeyNotFoundException();

        enrollment.Cancel();
        await _enrollments.SaveChangesAsync();
    }
}
