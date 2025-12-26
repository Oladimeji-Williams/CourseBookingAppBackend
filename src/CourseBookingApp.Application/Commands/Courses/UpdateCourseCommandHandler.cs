using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Courses;

public sealed class UpdateCourseCommandHandler
{
    private readonly ICourseRepository _repo;

    public UpdateCourseCommandHandler(ICourseRepository repo)
    {
        _repo = repo;
    }

    public async Task<CourseDto?> Handle(UpdateCourseCommand command)
    {
        var course = await _repo.GetByIdAsync(command.CourseId);
        if (course is null) return null;

        CourseMapper.MapUpdate(course, command.Dto);
        await _repo.SaveChangesAsync();

        return CourseMapper.ToDto(course);
    }
}
