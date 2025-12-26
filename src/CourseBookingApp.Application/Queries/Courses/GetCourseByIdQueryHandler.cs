using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;

namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Queries.Courses;

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
