using System.Security.Claims;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Commands.Users;
using CourseBookingAppBackend.src.CourseBookingApp.Application.DTOs;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Queries.Users;
using CourseBookingAppBackend.src.CourseBookingApp.Application.Validators;
using CourseBookingAppBackend.src.CourseBookingApp.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseBookingAppBackend.src.CourseBookingApp.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private int UserId =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    private UserType UserRole =>
        Enum.Parse<UserType>(
            User.FindFirstValue(ClaimTypes.Role)!);

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetUsers(
        [FromServices] GetUsersQueryHandler handler)
        => Ok(await handler.Handle());

    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser(
        int id,
        [FromServices] GetUserByIdQueryHandler handler)
        => Ok(await handler.Handle(
            new GetUserByIdQuery(id, UserId, UserRole)));

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateUserDto dto,
        [FromServices] UpdateUserCommandHandler handler)
        => Ok(await handler.Handle(
            new UpdateUserCommand(id, UserId, UserRole, dto)));

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(
        ChangePasswordDto dto,
        [FromServices] ChangePasswordCommandHandler handler)
    {
        await handler.Handle(
            new ChangePasswordCommand(
                UserId,
                dto.CurrentPassword,
                dto.NewPassword));

        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:int}/role")]
    public async Task<IActionResult> ChangeRole(
        int id,
        ChangeRoleDto dto,
        [FromServices] ChangeUserRoleCommandHandler handler)
        => Ok(await handler.Handle(
            new ChangeUserRoleCommand(id, UserId, dto.Type)));

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id,
        [FromServices] DeleteUserCommandHandler handler)
    {
        await handler.Handle(
            new DeleteUserCommand(id, UserId));

        return NoContent();
    }

    [Authorize]
    [HttpPost("{id:int}/upload-image")]
    public async Task<IActionResult> UploadImage(
        int id,
        IFormFile file,
        [FromServices] UploadUserImageCommandHandler handler)
    {
        ImageValidators.Validate(file);

        var url = await handler.Handle(
            new UploadUserImageCommand(
                id,
                UserId,
                UserRole,
                file.OpenReadStream(),
                file.ContentType,
                file.FileName));

        return Ok(new { imageUrl = url });
    }
}
