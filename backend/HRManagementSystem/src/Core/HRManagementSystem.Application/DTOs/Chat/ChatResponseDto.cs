namespace HRManagementSystem.Application.DTOs.Chat;

public class ChatResponseDto
{
    public required string Answer { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
