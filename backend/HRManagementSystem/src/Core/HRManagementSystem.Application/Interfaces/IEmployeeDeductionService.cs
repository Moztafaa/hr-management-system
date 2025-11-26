using HRManagementSystem.Application.DTOs.Payroll.EmployeeDeduction;

namespace HRManagementSystem.Application.Interfaces;

public interface IEmployeeDeductionService
{
    Task<Response<IEnumerable<EmployeeDeductionDto>>> GetAllAsync();
    Task<Response<EmployeeDeductionDto>> GetByIdAsync(int id);
    Task<Response<EmployeeDeductionDto>> CreateAsync(CreateEmployeeDeductionDto request);
    Task<Response<EmployeeDeductionDto>> UpdateAsync(int id, UpdateEmployeeDeductionDto request);
    Task<Response<bool>> DeleteAsync(int id);
    Task<Response<IEnumerable<EmployeeDeductionDto>>> GetByEmployeeAndMonthAsync(int employeeId, int month, int year);
    //Task<Response<decimal>> GetTotalDeductionsByEmployeeAndMonthAsync(int employeeId, int month, int year);
}
