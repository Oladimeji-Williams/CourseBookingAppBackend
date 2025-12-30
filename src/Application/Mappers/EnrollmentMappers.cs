using CourseBookingAppBackend.src.Application.DTOs;
using CourseBookingAppBackend.src.Domain.Entities;

namespace CourseBookingAppBackend.src.Application.Mappers;

internal static class EnrollmentMapper
{
    public static EnrollmentDto ToDto(Enrollment enrollment)
    {
        return new EnrollmentDto
        {
            Id = enrollment.Id,
            IsActive = enrollment.IsActive,
            UserId = enrollment.UserId,
            CourseId = enrollment.CourseId
        };
    }
}
