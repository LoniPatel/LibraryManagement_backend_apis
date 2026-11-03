
using LibraryManagement.Application.Common;
using LibraryManagement.Application.DTOs.Notifications;
using LibraryManagement.Application.Queries.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagement.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/notifications
    [HttpGet]
    public async Task<IActionResult> GetMyNotifications()
    {
        var userIdValue =
            User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdValue, out var userId))
        {
            return Unauthorized(new ApiResponse<object>
            {
                Success = false,
                Message = "Invalid user identity.",
                Data = null
            });
        }

        var query = new GetMyNotificationsQuery(userId);
        var notifications = await _mediator.Send(query);

        return Ok(new ApiResponse<IEnumerable<NotificationResponseDto>>
        {
            Success = true,
            Message = "Notifications received successfully.",
            Data = notifications
        });
    }
}
