using HRManagementSystem.Domain.Enums;
using HRManagementSystem.Domain.Enums.Notification;

namespace HRManagementSystem.Application.DTOs.Notification;

public class CreateNotificationDto
{
    public string? RecipientUserId { get; set; }
    public required string Title { get; set; }
    public required string Message { get; set; }
    public NotificationType Type { get; set; } = NotificationType.Info;
    public NotificationCategory Category { get; set; }
    public string? ActionUrl { get; set; }
    public int? RelatedEntityId { get; set; }
    public string? RelatedEntityType { get; set; }
    public NotificationPriority Priority { get; set; } = NotificationPriority.Normal;
}
