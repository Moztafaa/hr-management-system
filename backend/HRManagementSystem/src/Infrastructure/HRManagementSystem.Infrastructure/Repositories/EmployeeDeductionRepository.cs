namespace HRManagementSystem.Infrastructure.Repositories;

public class EmployeeDeductionRepository(ApplicationDbContext _context) : Repository<EmployeeDeduction>(_context), IEmployeeDeductionRepository
{
    public async Task<IEnumerable<EmployeeDeduction>> GetByEmployeeAndMonthAsync(
           int employeeId, int month, int year, CancellationToken cancellationToken = default) => await _dbSet
            .Where(ed => ed.EmployeeId == employeeId && ed.Month == month && ed.Year == year)
            .Include(ed => ed.Deduction)
            .ToListAsync(cancellationToken);

    public async Task<decimal> GetTotalDeductionsByEmployeeAndMonthAsync(
        int employeeId, int month, int year, CancellationToken cancellationToken = default) => await _dbSet
            .Where(ed => ed.EmployeeId == employeeId && ed.Month == month && ed.Year == year)
            .SumAsync(ed => ed.Amount, cancellationToken);
}
