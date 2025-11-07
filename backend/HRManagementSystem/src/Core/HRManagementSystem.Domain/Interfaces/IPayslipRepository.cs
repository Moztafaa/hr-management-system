
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Domain.Interfaces;
public interface IPayslipRepository : IRepository<Payslip>
{
    Task<bool> ExistsAsync(int employeeId, int month, int year);
    Task<IEnumerable<Payslip>> GetByEmployeeAsync(int employeeId);
    Task<Payslip?> GetByEmployeeAndDateAsync(int employeeId, int month, int year);
}
