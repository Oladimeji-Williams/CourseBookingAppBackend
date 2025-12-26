using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;

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
