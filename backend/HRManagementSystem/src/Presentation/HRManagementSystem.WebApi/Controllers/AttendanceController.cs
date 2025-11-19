using HRManagementSystem.Application.DTOs.Account;

namespace HRManagementSystem.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AttendanceController(IAttendanceService _attendanceService) : ControllerBase
{

    [HttpPost("check-in")]
    public async Task<IActionResult> CheckIn(CheckInRequestDto request)
    {
        Response<int> result = await _attendanceService.CheckInAsync(request.EmployeeId, request.Base64Image);
        if (result.HasError)
            return Ok(new { errorMassage = result.ErrorMessage, id = result.Data });

        return Ok(new { errorMassage = result.ErrorMessage, id = result.Data });
    }

    [HttpPost("check-out")]
    public async Task<IActionResult> CheckOut(CheckOutRequestDto request)
    {
        Response<int> result = await _attendanceService.CheckOutAsync(request.EmployeeId, request.Base64Image);
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

