using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Interfaces;

namespace HRManagementSystem.Application.Interfaces;

public interface IEmployeeDeductionRepository : IRepository<EmployeeDeduction>
{
    Task<IEnumerable<EmployeeDeduction>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<decimal> GetTotalDeductionsByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
}
