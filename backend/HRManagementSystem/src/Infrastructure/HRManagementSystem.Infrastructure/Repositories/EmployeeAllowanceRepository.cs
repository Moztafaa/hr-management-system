namespace HRManagementSystem.Infrastructure.Repositories;

public class EmployeeAllowanceRepository(ApplicationDbContext _context) : Repository<EmployeeAllowance>(_context), IEmployeeAllowanceRepository
{
    public async Task<IEnumerable<EmployeeAllowance>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(ea => ea.EmployeeId == employeeId)
            .Include(ea => ea.Allowance)
            .ToListAsync(cancellationToken);
    }

    public async Task<decimal> GetTotalAllowancesByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(ea => ea.EmployeeId == employeeId)
            .SumAsync(ea => ea.Amount, cancellationToken);
    }
}
