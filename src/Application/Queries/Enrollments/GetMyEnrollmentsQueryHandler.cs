using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Application.DTOs;
using CourseBookingAppBackend.src.Application.Mappers;

namespace CourseBookingAppBackend.src.Application.Queries.Enrollments;

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
