using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Application.DTOs;
using CourseBookingAppBackend.src.Application.Mappers;

namespace CourseBookingAppBackend.src.Application.Queries.Courses;

public sealed class GetCourseByIdQueryHandler
{
    private readonly ICourseRepository _repo;

    public GetCourseByIdQueryHandler(ICourseRepository repo)
    {
        _repo = repo;
    }

    public async Task<CourseDto?> Handle(GetCourseByIdQuery query)
    {
        var course = await _repo.GetByIdAsync(query.Id);
        return course == null ? null : CourseMapper.ToDto(course);
    }
}
