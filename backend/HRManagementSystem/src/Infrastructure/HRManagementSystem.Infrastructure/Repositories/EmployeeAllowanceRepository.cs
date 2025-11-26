namespace HRManagementSystem.Infrastructure.Repositories;

public class EmployeeAllowanceRepository(ApplicationDbContext _context) : Repository<EmployeeAllowance>(_context), IEmployeeAllowanceRepository
{
    public async Task<IEnumerable<EmployeeAllowance>> GetByEmployeeAndMonthAsync(
          int employeeId, int month, int year, CancellationToken cancellationToken = default) => await _dbSet
              .Where(ea => ea.EmployeeId == employeeId && ea.Month == month && ea.Year == year)
              .Include(ea => ea.Allowance)
              .ToListAsync(cancellationToken);

    //public async Task<decimal> GetTotalAllowancesByEmployeeAndMonthAsync(
    //    int employeeId, int month, int year, CancellationToken cancellationToken = default) => await _dbSet
    //        .Where(ea => ea.EmployeeId == employeeId && ea.Month == month && ea.Year == year)
    //        .SumAsync(ea => ea.Amount, cancellationToken);

    public async Task<EmployeeAllowance?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default) => await _dbSet
            .Include(ea => ea.Allowance)
            .Include(ea => ea.Employee)
            .FirstOrDefaultAsync(ea => ea.Id == id, cancellationToken);

    public async Task<IEnumerable<EmployeeAllowance>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default) => await _dbSet
            .Include(ea => ea.Allowance)
            .Include(ea => ea.Employee)
            .ToListAsync(cancellationToken);


}
