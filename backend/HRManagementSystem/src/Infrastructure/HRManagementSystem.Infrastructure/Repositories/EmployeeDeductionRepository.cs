namespace HRManagementSystem.Infrastructure.Repositories;

public class EmployeeDeductionRepository(ApplicationDbContext _context) : Repository<EmployeeDeduction>(_context), IEmployeeDeductionRepository
{
    public async Task<IEnumerable<EmployeeDeduction>> GetByEmployeeAndMonthAsync(
           int employeeId, int month, int year, CancellationToken cancellationToken = default) => await _dbSet
            .Where(ed => ed.EmployeeId == employeeId && ed.Month == month && ed.Year == year)
            .Include(ed => ed.Deduction)
            .ToListAsync(cancellationToken);

    //public async Task<decimal> GetTotalDeductionsByEmployeeAndMonthAsync(
    //    int employeeId, int month, int year, CancellationToken cancellationToken = default) => await _dbSet
    //        .Where(ed => ed.EmployeeId == employeeId && ed.Month == month && ed.Year == year)
    //        .SumAsync(ed => ed.Amount, cancellationToken);

    public async Task<EmployeeDeduction?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default) => await _dbSet
            .Include(ed => ed.Deduction)
            .Include(ed => ed.Employee)
            .FirstOrDefaultAsync(ed => ed.Id == id, cancellationToken);

    public async Task<IEnumerable<EmployeeDeduction>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default) => await _dbSet
            .Include(ed => ed.Deduction)
            .Include(ed => ed.Employee)
            .ToListAsync(cancellationToken);

}
