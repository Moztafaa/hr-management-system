using System;

namespace HRManagementSystem.Infrastructure.Repositories;

public class DepartmentRepository(ApplicationDbContext _context) : Repository<Department>(_context), IDepartmentRepository
{
    public override async Task<IEnumerable<Department>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .Include(d => d.Employees)
            .ToListAsync(cancellationToken);
    }

    public async Task<Department> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .FirstOrDefaultAsync(d => d.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Department>> GetDepartmentsWithEmployeesAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Departments
            .Where(d => d.Id == id)
            .Include(d => d.Employees)
            .ToListAsync(cancellationToken);
    }
}
