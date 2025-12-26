using CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Courses;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Queries.Courses;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseBookingAppBackend.src.CourseBookingApp.API.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromServices] GetCoursesQueryHandler handler)
        => Ok(await handler.Handle(new()));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(
        int id,
        [FromServices] GetCourseByIdQueryHandler handler)
    {
        var result = await handler.Handle(new(id));
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateCourseDto dto,
        [FromServices] CreateCourseCommandHandler handler)
        => Ok(await handler.Handle(new(dto)));

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateCourseDto dto,
        [FromServices] UpdateCourseCommandHandler handler)
    {
        var result = await handler.Handle(new(id, dto));
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        [FromServices] DeleteCourseCommandHandler handler)
        => await handler.Handle(new(id)) ? NoContent() : NotFound();

    [Authorize(Roles = "Admin")]
    [HttpPost("{id:int}/upload-image")]
    public async Task<IActionResult> UploadImage(
        int id,
        IFormFile file,
        [FromServices] UploadCourseImageCommandHandler handler)
    {
        ImageValidators.Validate(file);

        var url = await handler.Handle(
            new(
                id,
                file.OpenReadStream(),
                file.ContentType,
                file.FileName
            ));

        return url is null ? NotFound() : Ok(new { imageUrl = url });
    }
}
