namespace HRManagementSystem.Infrastructure.Repositories;

public class DeductionRepository(ApplicationDbContext _context) : Repository<Deduction>(_context), IDeductionRepository
{
    public async Task<IEnumerable<Deduction>> GetByMinAmountAsync(decimal minAmount, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(d => d.Amount >= minAmount)
            .ToListAsync(cancellationToken);
    }

    public async Task<Deduction?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower(), cancellationToken);
    }
}
