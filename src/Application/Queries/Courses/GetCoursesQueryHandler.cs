using CourseBookingAppBackend.src.Application.Abstractions.Persistence;
using CourseBookingAppBackend.src.Application.DTOs;

namespace CourseBookingAppBackend.src.Application.Queries.Courses;

public sealed class GetCoursesQueryHandler
{
    private readonly ICourseRepository _repo;

    public GetCoursesQueryHandler(ICourseRepository repo)
    {
        _repo = repo;
    }

    public async Task<PagedResult<CourseDto>> Handle(GetCoursesQuery query)
    {
        return await _repo.GetAllAsync(query.Params);
    }
}
