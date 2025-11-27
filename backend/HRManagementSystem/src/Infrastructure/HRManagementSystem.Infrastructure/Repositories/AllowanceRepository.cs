namespace HRManagementSystem.Infrastructure.Repositories;

public class AllowanceRepository(ApplicationDbContext _context) : Repository<Allowance>(_context), IAllowanceRepository
{
    public async Task<IEnumerable<Allowance>> GetByMinAmountAsync(decimal minAmount, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(a => a.Amount >= minAmount)
            .ToListAsync(cancellationToken);
    }

    public async Task<Allowance?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower(), cancellationToken);
    }
}
