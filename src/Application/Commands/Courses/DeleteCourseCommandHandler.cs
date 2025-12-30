using CourseBookingAppBackend.src.Application.Abstractions.Persistence;

namespace CourseBookingAppBackend.src.Application.Commands.Courses;

public sealed class DeleteCourseCommandHandler
{
    private readonly ICourseRepository _repo;

    public DeleteCourseCommandHandler(ICourseRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteCourseCommand command)
    {
        var course = await _repo.GetByIdAsync(command.CourseId);
        if (course == null) return false;

        await _repo.RemoveAsync(course);
        await _repo.SaveChangesAsync();
        return true;
    }
}

