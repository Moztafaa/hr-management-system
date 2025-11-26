using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Interfaces;

namespace HRManagementSystem.Application.Interfaces;

public interface IEmployeeDeductionRepository : IRepository<EmployeeDeduction>
{
    Task<IEnumerable<EmployeeDeduction>> GetByEmployeeAndMonthAsync(
           int employeeId, int month, int year, CancellationToken cancellationToken = default);
    //Task<decimal> GetTotalDeductionsByEmployeeAndMonthAsync(
    //    int employeeId, int month, int year, CancellationToken cancellationToken = default);

    Task<EmployeeDeduction?> GetByIdWithDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<EmployeeDeduction>> GetAllWithDetailsAsync(CancellationToken cancellationToken = default);
}
