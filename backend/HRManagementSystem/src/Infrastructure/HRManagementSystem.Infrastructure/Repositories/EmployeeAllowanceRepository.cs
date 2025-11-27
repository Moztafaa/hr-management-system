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
            .SumAsync(ea => ea.Amount ?? 0m, cancellationToken);
    }

    public async Task<EmployeeAllowance?> GetByCompositeKeyAsync(int employeeId, int allowanceId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(ea => ea.EmployeeId == employeeId && ea.AllowanceId == allowanceId, cancellationToken);
    }

    public async Task DeleteAsync(int employeeId, int allowanceId, CancellationToken cancellationToken = default)
    {
        var entity = await GetByCompositeKeyAsync(employeeId, allowanceId, cancellationToken);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public override Task<EmployeeAllowance?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<EmployeeAllowance?>(null);
    }
}
