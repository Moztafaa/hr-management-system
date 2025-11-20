namespace HRManagementSystem.Application.Interfaces;
public interface IAttendanceService
{
    Task<Response<int>> CheckInAsync(int employeeId, byte[] image);
    Task<Response<int>> CheckOutAsync(int employeeId, byte[] image);
    Task<Response<IEnumerable<AttendanceEmployeeDto>>> GetEmployeeAttendanceAsync(int employeeId);
}
