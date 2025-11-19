namespace HRManagementSystem.Infrastructure.Repositories;

public class PayslipRepository : Repository<Payslip>, IPayslipRepository
{
    public PayslipRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Payslip>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(p => p.EmployeeId == employeeId)
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeAllowances)
                    .ThenInclude(ea => ea.Allowance)
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeDeductions)
                    .ThenInclude(ed => ed.Deduction)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Payslip>> GetByMonthAsync(int month, int year, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeAllowances)
                    .ThenInclude(ea => ea.Allowance)
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeDeductions)
                    .ThenInclude(ed => ed.Deduction)
            .Where(p => p.Month == month && p.Year == year)
            .ToListAsync(cancellationToken);
    }

    public async Task<Payslip?> GetByEmployeeAndMonthAsync(int employeeId, int month, int year, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeAllowances)
                    .ThenInclude(ea => ea.Allowance)
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeDeductions)
                    .ThenInclude(ed => ed.Deduction)
            .FirstOrDefaultAsync(p =>
                p.EmployeeId == employeeId &&
                p.Month == month &&
                p.Year == year, cancellationToken);
    }
}
