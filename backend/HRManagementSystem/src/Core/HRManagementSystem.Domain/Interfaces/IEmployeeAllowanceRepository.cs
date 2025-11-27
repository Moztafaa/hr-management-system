using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;

public interface IEmployeeAllowanceRepository:IRepository<EmployeeAllowance>
{
    Task<IEnumerable<EmployeeAllowance>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);

    Task<decimal> GetTotalAllowancesByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);

    Task<EmployeeAllowance?> GetByCompositeKeyAsync(int employeeId, int allowanceId, CancellationToken cancellationToken = default);
    Task DeleteAsync(int employeeId, int allowanceId, CancellationToken cancellationToken = default);

}
