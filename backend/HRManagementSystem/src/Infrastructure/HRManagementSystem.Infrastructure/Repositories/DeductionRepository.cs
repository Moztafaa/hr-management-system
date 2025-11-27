namespace HRManagementSystem.Infrastructure.Repositories;

public class DeductionRepository(ApplicationDbContext _context) : Repository<Deduction>(_context), IDeductionRepository
{
    public async Task<IEnumerable<Deduction>> GetByMinValueAsync(decimal minValue, CancellationToken cancellationToken = default) => await _dbSet
            .Where(d => d.Value >= minValue)
            .ToListAsync(cancellationToken);

    public async Task<Deduction?> GetByNameAsync(string name, CancellationToken cancellationToken = default) => await _dbSet
            .FirstOrDefaultAsync(d => d.Name.ToLower() == name.ToLower(), cancellationToken);

    public async Task<IEnumerable<Deduction>> GetFixedDeductionsAsync(CancellationToken cancellationToken = default) => await _dbSet
            .Where(d => d.IsFixed)
            .ToListAsync(cancellationToken);

}
