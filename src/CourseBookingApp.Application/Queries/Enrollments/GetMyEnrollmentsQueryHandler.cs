using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Queries.Enrollments;

public sealed class GetMyEnrollmentsQueryHandler
{
    private readonly IEnrollmentRepository _enrollments;

    public GetMyEnrollmentsQueryHandler(IEnrollmentRepository enrollments)
    {
        _enrollments = enrollments;
    }

    public async Task<IEnumerable<EnrollmentDto>> Handle(GetMyEnrollmentsQuery query)
    {
        var enrollments = await _enrollments
            .GetActiveUserEnrollmentsAsync(query.UserId);

        return enrollments.Select(EnrollmentMapper.ToDto);
    }
}
