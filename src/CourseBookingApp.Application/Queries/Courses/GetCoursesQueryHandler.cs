using CourseBookingAppBackend.src.CourseBookingApp.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Mappers;
namespace CourseBookingAppBackend.src.CourseBookingApp.Application.Queries.Courses;

public sealed class GetCoursesQueryHandler
{
    private readonly ICourseRepository _repo;

    public GetCoursesQueryHandler(ICourseRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<CourseDto>> Handle(GetCoursesQuery _)
    {
        var courses = await _repo.GetAllAsync();
        return courses.Select(CourseMapper.ToDto);
    }
}
