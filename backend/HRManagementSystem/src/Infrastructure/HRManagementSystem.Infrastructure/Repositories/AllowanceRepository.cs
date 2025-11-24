
namespace HRManagementSystem.Infrastructure.Repositories;

public class AllowanceRepository(ApplicationDbContext _context) : Repository<Allowance>(_context), IAllowanceRepository
{
    public async Task<IEnumerable<Allowance>> GetByMinValueAsync(decimal minValue, CancellationToken cancellationToken = default) => await _dbSet
            .Where(a => a.Value >= minValue)
            .ToListAsync(cancellationToken);

    public async Task<Allowance?> GetByNameAsync(string name, CancellationToken cancellationToken = default) => await _dbSet
            .FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower(), cancellationToken);
}
