
namespace HRManagementSystem.Infrastructure.Repositories;
public class PayslipRepository : Repository<Payslip>, IPayslipRepository
{
    public PayslipRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsAsync(int employeeId, int month, int year)
    {
        return await _dbSet.AnyAsync(p =>
               p.EmployeeId == employeeId &&
               p.Month == month &&
               p.Year == year
               );

    }

    public async Task<IEnumerable<Payslip>> GetByEmployeeAsync(int employeeId)
    {
        return await _dbSet
            .Where(p => p.EmployeeId == employeeId)
            .Include(p => p.Employee)
            .ToListAsync();
    }
    public async Task<Payslip?> GetByEmployeeAndDateAsync(int employeeId, int month, int year)
    {
        return await _dbSet
            .Include(p => p.Employee)
            .FirstOrDefaultAsync(p =>
                p.EmployeeId == employeeId &&
                p.Month == month &&
                p.Year == year
            );
    }

}
