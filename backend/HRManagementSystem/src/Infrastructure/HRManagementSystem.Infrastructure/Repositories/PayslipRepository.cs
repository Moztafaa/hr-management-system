namespace HRManagementSystem.Infrastructure.Repositories;

public class PayslipRepository(ApplicationDbContext context) : Repository<Payslip>(context), IPayslipRepository
{
    public async Task<IEnumerable<Payslip>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default) => await _dbSet
            .Where(p => p.EmployeeId == employeeId)
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeAllowances)
                    .ThenInclude(ea => ea.Allowance)
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeDeductions)
                    .ThenInclude(ed => ed.Deduction)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Payslip>> GetByMonthAsync(int month, int year, CancellationToken cancellationToken = default) => await _dbSet
            .Where(p => p.Month == month && p.Year == year)
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeAllowances)
                    .ThenInclude(ea => ea.Allowance)
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeDeductions)
                    .ThenInclude(ed => ed.Deduction)
            .ToListAsync(cancellationToken);

    public async Task<Payslip?> GetByEmployeeAndMonthAsync(int employeeId, int month, int year, CancellationToken cancellationToken = default) => await _dbSet
            .Where(p => p.EmployeeId == employeeId && p.Month == month && p.Year == year)
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeAllowances)
                    .ThenInclude(ea => ea.Allowance)
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeDeductions)
                    .ThenInclude(ed => ed.Deduction)
            .FirstOrDefaultAsync(cancellationToken);

    public override async Task<Payslip?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _dbSet
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeAllowances)
                    .ThenInclude(ea => ea.Allowance)
            .Include(p => p.Employee)
                .ThenInclude(e => e.EmployeeDeductions)
                    .ThenInclude(ed => ed.Deduction)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);


}
