namespace HRManagementSystem.Application.Interfaces;
public interface IAttendanceService
{
    Task<Response<int>> CheckInAsync(int employeeId, string base64Image);
    Task<Response<int>> CheckOutAsync(int employeeId, string base64Image);
    Task<Response<IEnumerable<AttendanceEmployeeDto>>> GetEmployeeAttendanceAsync(int employeeId);
}
