namespace HRManagementSystem.Application.DTOs.Notification;

public class NotificationDto
{
    public int Id { get; set; }
    public string? RecipientUserId { get; set; }
    public required string Title { get; set; }
    public required string Message { get; set; }
    public string Type { get; set; } = null!;
    public string Category { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }
    public string? ActionUrl { get; set; }
    public int? RelatedEntityId { get; set; }
    public string? RelatedEntityType { get; set; }
    public string Priority { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
