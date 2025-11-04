using System;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<IEnumerable<Department>> GetDepartmentsWithEmployeesAsync(int id, CancellationToken cancellationToken = default);
    Task<Department> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
