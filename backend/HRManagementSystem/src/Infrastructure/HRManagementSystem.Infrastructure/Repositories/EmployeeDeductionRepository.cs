namespace HRManagementSystem.Infrastructure.Repositories;

public class EmployeeDeductionRepository(ApplicationDbContext _context)
    : Repository<EmployeeDeduction>(_context), IEmployeeDeductionRepository
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
            .SumAsync(ed => ed.Amount ?? 0m, cancellationToken);
    }

    public async Task<EmployeeDeduction?> GetByCompositeKeyAsync(int employeeId, int deductionId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(ed => ed.EmployeeId == employeeId && ed.DeductionId == deductionId, cancellationToken);
    }

    public async Task DeleteAsync(int employeeId, int deductionId, CancellationToken cancellationToken = default)
    {
        var entity = await GetByCompositeKeyAsync(employeeId, deductionId, cancellationToken);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public override Task<EmployeeDeduction?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<EmployeeDeduction?>(null);
    }
}
