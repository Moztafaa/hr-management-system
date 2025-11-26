using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;

public interface IEmployeeAllowanceRepository:IRepository<EmployeeAllowance>
{
    Task<IEnumerable<EmployeeAllowance>> GetByEmployeeAndMonthAsync(
          int employeeId, int month, int year, CancellationToken cancellationToken = default);
    //Task<decimal> GetTotalAllowancesByEmployeeAndMonthAsync(
    //    int employeeId, int month, int year, CancellationToken cancellationToken = default);
    Task<EmployeeAllowance?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<EmployeeAllowance>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
}
