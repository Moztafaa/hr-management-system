using Microsoft.AspNetCore.Http;

namespace HRManagementSystem.Application.DTOs.Attendance;
public class CheckInRequestDto
{
    public IFormFile Image { get; set; }
}
