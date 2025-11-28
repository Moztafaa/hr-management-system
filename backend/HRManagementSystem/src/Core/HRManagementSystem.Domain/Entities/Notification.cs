using System;
using HRManagementSystem.Domain.Enums.Notification;

namespace HRManagementSystem.Domain.Entities;

public class Notification : BaseEntity
{
    public string? RecipientUserId { get; set; }  // Nullable for system-wide notifications
    public required string Title { get; set; }
    public required string Message { get; set; }
    public NotificationType Type { get; set; }
    public NotificationCategory Category { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime? ReadAt { get; set; }
    public string? ActionUrl { get; set; }  // Link to related resource
    public int? RelatedEntityId { get; set; }  // e.g., LeaveRequestId, JobApplicationId
    public string? RelatedEntityType { get; set; }  // e.g., "LeaveRequest", "JobApplication"
    public NotificationPriority Priority { get; set; } = NotificationPriority.Normal;

}
