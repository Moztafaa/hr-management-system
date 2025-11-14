using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;

public interface IPayslipRepository : IRepository<Payslip>
{
    Task<IEnumerable<Payslip>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Payslip>> GetByMonthAsync(int month, int year, CancellationToken cancellationToken = default);
    Task<Payslip?> GetByEmployeeAndMonthAsync(int employeeId, int month, int year, CancellationToken cancellationToken = default);
}
