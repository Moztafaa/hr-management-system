namespace HRManagementSystem.Application.DTOs.Email;

public class SendEmailRequestDto
{
    public required string To { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
    public bool IsHtml { get; set; } = true;
}
