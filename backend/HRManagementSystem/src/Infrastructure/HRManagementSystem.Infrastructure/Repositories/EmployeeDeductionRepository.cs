namespace HRManagementSystem.Infrastructure.Repositories;

public class EmployeeDeductionRepository(ApplicationDbContext _context) : Repository<EmployeeDeduction>(_context), IEmployeeDeductionRepository
{
    public async Task<IEnumerable<EmployeeDeduction>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(ed => ed.EmployeeId == employeeId)
            .Include(ed => ed.Deduction)
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal> GetTotalDeductionsByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(ed => ed.EmployeeId == employeeId)
            .SumAsync(ed => ed.Amount, cancellationToken);
    }
}
