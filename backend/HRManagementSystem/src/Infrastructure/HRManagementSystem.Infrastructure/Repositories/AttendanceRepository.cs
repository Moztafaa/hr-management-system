namespace HRManagementSystem.Infrastructure.Repositories;
public class AttendanceRepository(ApplicationDbContext _context) : Repository<Attendance>(_context), IAttendanceRepository
{
    public async Task<Attendance?> GetTodayAttendanceAsync(int employeeId, DateTime date)
    {
        return await _context.Attendances
            .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.Date.Date == date.Date);
    }
    public async Task<IEnumerable<Attendance>> GetByEmployeeAsync(int employeeId)
    {
        return await _context.Attendances
                .Where(a => a.EmployeeId == employeeId)
                .ToListAsync();
    }
}
