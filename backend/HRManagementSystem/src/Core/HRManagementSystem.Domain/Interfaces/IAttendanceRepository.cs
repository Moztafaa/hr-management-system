using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;
public interface IAttendanceRepository: IRepository<Attendance>
{
    Task<Attendance?> GetTodayAttendanceAsync(int employeeId, DateTime date);
    Task<IEnumerable<Attendance>> GetByEmployeeAsync(int employeeId);
}
