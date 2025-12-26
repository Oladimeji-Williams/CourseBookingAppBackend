using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Entities;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Enrollments;

public sealed class EnrollInCourseCommandHandler
{
    private readonly IEnrollmentRepository _enrollments;
    private readonly ICourseRepository _courses;

    public EnrollInCourseCommandHandler(
        IEnrollmentRepository enrollments,
        ICourseRepository courses)
    {
        _enrollments = enrollments;
        _courses = courses;
    }

    public async Task<EnrollmentDto> Handle(EnrollInCourseCommand command)
    {
        var course = await _courses.GetByIdAsync(command.CourseId)
            ?? throw new KeyNotFoundException("Course not found");

        var existing = await _enrollments
            .GetEnrollmentAsync(command.UserId, command.CourseId);

        if (existing is { IsActive: true })
            throw new InvalidOperationException("Already enrolled");

        var enrollment = Enrollment.Create(command.UserId, command.CourseId);

        await _enrollments.AddEnrollmentAsync(enrollment);
        await _enrollments.SaveChangesAsync();

        return EnrollmentMapper.ToDto(enrollment);
    }
}
