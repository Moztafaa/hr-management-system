namespace HRManagementSystem.Application.Interfaces;

public interface IEmployeeDeductionService
{
    Task<Response<IEnumerable<EmployeeDeductionDto>>> GetAllAsync();
    Task<Response<EmployeeDeductionDto>> GetByCompositeKeyAsync(int employeeId, int deductionId);
    Task<Response<IEnumerable<EmployeeDeductionWithDetailsDto>>> GetByEmployeeIdAsync(int employeeId);
    Task<Response<EmployeeDeductionDto>> CreateAsync(CreateEmployeeDeductionDto request);
    Task<Response<EmployeeDeductionDto>> UpdateAsync(int employeeId, int deductionId, UpdateEmployeeDeductionDto request);
    Task<Response<bool>> DeleteAsync(int employeeId, int deductionId);
}
