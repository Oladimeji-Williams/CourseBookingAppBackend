using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Application.DTOs;
using CourseBookingAppBackend.src.Application.Mappers;

namespace CourseBookingAppBackend.src.Application.Commands.Courses;

public sealed class CreateCourseCommandHandler
{
    private readonly ICourseRepository _repo;

    public CreateCourseCommandHandler(ICourseRepository repo)
    {
        _repo = repo;
    }

    public async Task<CourseDto> Handle(CreateCourseCommand command)
    {
        var course = CourseMapper.ToEntity(command.Dto);

        await _repo.AddAsync(course);
        await _repo.SaveChangesAsync();

        return CourseMapper.ToDto(course);
    }
}
