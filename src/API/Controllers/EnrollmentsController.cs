using System.Security.Claims;
using CourseBookingAppBackend.src.Application.Commands.Enrollments;
using CourseBookingAppBackend.src.Application.Queries.Enrollments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseBookingAppBackend.src.API.Controllers;

[ApiController]
[Authorize]
[Route("api/enrollments")]
public class EnrollmentsController(
    EnrollInCourseCommandHandler enroll,
    CancelEnrollmentCommandHandler cancel,
    GetMyEnrollmentsQueryHandler getMine) : ControllerBase
{
    private readonly EnrollInCourseCommandHandler _enroll = enroll;
    private readonly CancelEnrollmentCommandHandler _cancel = cancel;
    private readonly GetMyEnrollmentsQueryHandler _getMine = getMine;

  private int UserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost("{courseId:int}")]
    public async Task<IActionResult> Enroll(
        int courseId,
        [FromServices] EnrollInCourseCommandHandler handler)
    {
        var result = await handler.Handle(
            new EnrollInCourseCommand(UserId, courseId));

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> MyEnrollments(
        [FromServices] GetMyEnrollmentsQueryHandler handler)
    {
        return Ok(await handler.Handle(
            new GetMyEnrollmentsQuery(UserId)));
    }

    [HttpDelete("{courseId:int}")]
    public async Task<IActionResult> Cancel(
        int courseId,
        [FromServices] CancelEnrollmentCommandHandler handler)
    {
        await handler.Handle(
            new CancelEnrollmentCommand(UserId, courseId));

        return NoContent();
    }
}
