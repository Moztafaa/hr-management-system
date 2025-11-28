namespace HRManagementSystem.Infrastructure.Repositories;

public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Employee?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Email.ToLower() == email.ToLower(), cancellationToken);
    }

    public async Task<IEnumerable<Employee>> GetByDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(e => e.DepartmentId == departmentId)
            .Include(e => e.Department)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Employee>> GetByDesignationAsync(int designationId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(e => e.DesignationId == designationId)
            .Include(e => e.Designation)
            .ToListAsync(cancellationToken);
    }

}
