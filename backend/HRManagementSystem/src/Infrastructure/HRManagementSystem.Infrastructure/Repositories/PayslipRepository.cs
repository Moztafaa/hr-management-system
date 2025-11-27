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
    public async Task<IEnumerable<EmployeeAllowance>> GetActiveAllowancesAsync(int employeeId, int month, int year, CancellationToken cancellationToken = default)
    {
        var employee = await _context.Employees
            .Include(e => e.EmployeeAllowances)
                .ThenInclude(ea => ea.Allowance)
            .FirstOrDefaultAsync(e => e.Id == employeeId, cancellationToken);

        if (employee == null)
            return Enumerable.Empty<EmployeeAllowance>();

        return employee.EmployeeAllowances
            .Where(ea => IsAllowanceActive(ea, month, year))
            .ToList();
    }

    private bool IsAllowanceActive(EmployeeAllowance ea, int month, int year)
    {
        var date = new DateTime(year, month, 1);
        switch (ea.Recurrence)
        {
            case RecurrenceType.OneTime:
                return ea.StartDate.HasValue && ea.StartDate.Value.Month == month && ea.StartDate.Value.Year == year;
            case RecurrenceType.Period:
                return ea.StartDate.HasValue && ea.EndDate.HasValue && date >= new DateTime(ea.StartDate.Value.Year, ea.StartDate.Value.Month, 1)
                                                         && date <= new DateTime(ea.EndDate.Value.Year, ea.EndDate.Value.Month, 1);
            case RecurrenceType.Annual:
                return ea.StartDate.HasValue &&
                       ea.StartDate.Value.Month == month &&
                       year >= ea.StartDate.Value.Year;
            case RecurrenceType.Permanent:
                return true;
            default:
                return false;
        }
    }
    public async Task<IEnumerable<EmployeeDeduction>> GetActiveDeductionsAsync(int employeeId, int month, int year, CancellationToken cancellationToken = default)
    {
        var employee = await _context.Employees
            .Include(e => e.EmployeeDeductions)
                .ThenInclude(ed => ed.Deduction)
            .FirstOrDefaultAsync(e => e.Id == employeeId, cancellationToken);

        if (employee == null)
            return Enumerable.Empty<EmployeeDeduction>();

        return employee.EmployeeDeductions
            .Where(ed => IsDeductionActive(ed, month, year))
            .ToList();
    }

    private bool IsDeductionActive(EmployeeDeduction ed, int month, int year)
    {
        var date = new DateTime(year, month, 1);

        switch (ed.Recurrence)
        {
            case RecurrenceType.OneTime:
                return ed.StartDate.HasValue &&
                       ed.StartDate.Value.Month == month &&
                       ed.StartDate.Value.Year == year;

            case RecurrenceType.Period:
                if (!ed.StartDate.HasValue || !ed.EndDate.HasValue)
                    return false;

                var start = new DateTime(ed.StartDate.Value.Year, ed.StartDate.Value.Month, 1);
                var end = new DateTime(ed.EndDate.Value.Year, ed.EndDate.Value.Month, 1);

                return date >= start && date <= end;

            case RecurrenceType.Annual:
                return ed.StartDate.HasValue &&
                       ed.StartDate.Value.Month == month &&
                       year >= ed.StartDate.Value.Year;

            case RecurrenceType.Permanent:
                return true;

            default:
                return false;
        }
    }


}
