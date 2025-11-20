using HRManagementSystem.Application.DTOs.Account;

namespace HRManagementSystem.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AttendanceController(IAttendanceService _attendanceService) : ControllerBase
{

    [HttpPost("{employeeId}/check-in")]
    public async Task<IActionResult> CheckIn(int employeeId, CheckInRequestDto request)
    {
        using var ms = new MemoryStream();
        await request.Image.CopyToAsync(ms);
        Response<int> result = await _attendanceService.CheckInAsync(employeeId, ms.ToArray());
        if (result.HasError)
            return Ok(new { errorMassage = result.ErrorMessage, id = result.Data });

        return Ok(new { errorMassage = result.ErrorMessage, id = result.Data });
    }

    [HttpPost("{employeeId}/check-out")]
    public async Task<IActionResult> CheckOut(int employeeId, CheckOutRequestDto request)
    {
        using var ms = new MemoryStream();
        await request.Image.CopyToAsync(ms);
        Response<int> result = await _attendanceService.CheckOutAsync(employeeId, ms.ToArray());
        if (result.HasError)
            return Ok(new { errorMassage = result.ErrorMessage, id = result.Data });

        return Ok(new { errorMassage = result.ErrorMessage, id = result.Data });
    }

    [HttpGet("employee/{employeeId}")]
    public async Task<IActionResult> GetEmployeeAttendance(int employeeId)
    {
        var data = await _attendanceService.GetEmployeeAttendanceAsync(employeeId);
        return Ok(data);
    }
}

