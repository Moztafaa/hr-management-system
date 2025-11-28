using System.Security.Claims;
using HRManagementSystem.Application.DTOs.Notification;
using HRManagementSystem.Application.Interfaces;

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController(INotificationService notificationService) : ControllerBase
{
    [HttpGet("my")]
    public async Task<IActionResult> GetMyNotifications(CancellationToken cancellationToken)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { hasError = true, errorMessage = "User not authenticated" });
        }

        Response<IEnumerable<NotificationDto>> response = await notificationService.GetAllNotificationsAsync(userId, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpGet("my/unread")]
    public async Task<IActionResult> GetMyUnreadNotifications(CancellationToken cancellationToken)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { hasError = true, errorMessage = "User not authenticated" });
        }

        Response<IEnumerable<NotificationDto>> response = await notificationService.GetUnreadNotificationsAsync(userId, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpGet("my/unread-count")]
    public async Task<IActionResult> GetMyUnreadCount(CancellationToken cancellationToken)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { hasError = true, errorMessage = "User not authenticated" });
        }

        Response<int> response = await notificationService.GetUnreadCountAsync(userId, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotificationById(int id, CancellationToken cancellationToken)
    {
        Response<NotificationDto> response = await notificationService.GetNotificationByIdAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDto createDto, CancellationToken cancellationToken)
    {
        Response<NotificationDto> response = await notificationService.CreateNotificationAsync(createDto, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return CreatedAtAction(nameof(GetNotificationById), new { id = response.Data.Id }, response);
    }

    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(int id, CancellationToken cancellationToken)
    {
        Response<bool> response = await notificationService.MarkAsReadAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return NoContent();
    }

    [HttpPut("my/read-all")]
    public async Task<IActionResult> MarkAllAsRead(CancellationToken cancellationToken)
    {
        string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized(new { hasError = true, errorMessage = "User not authenticated" });
        }

        Response<bool> response = await notificationService.MarkAllAsReadAsync(userId, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(int id, CancellationToken cancellationToken)
    {
        Response<bool> response = await notificationService.DeleteNotificationAsync(id, cancellationToken);
        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }
        return NoContent();
    }
}