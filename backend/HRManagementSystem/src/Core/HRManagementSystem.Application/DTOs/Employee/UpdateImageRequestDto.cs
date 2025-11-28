using Microsoft.AspNetCore.Http;
namespace HRManagementSystem.Application.DTOs.Employee;
public class UpdateImageRequestDto
{
    public IFormFile Image { get; set; }
}
