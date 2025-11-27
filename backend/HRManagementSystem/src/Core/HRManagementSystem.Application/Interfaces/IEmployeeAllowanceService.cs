namespace HRManagementSystem.Application.Interfaces;

public interface IEmployeeAllowanceService
{
    Task<Response<IEnumerable<EmployeeAllowanceDto>>> GetAllAsync();
    Task<Response<EmployeeAllowanceDto>> GetByCompositeKeyAsync(int employeeId, int allowanceId);
    Task<Response<IEnumerable<EmployeeAllowanceWithDetailsDto>>> GetByEmployeeIdAsync(int employeeId);
    Task<Response<EmployeeAllowanceDto>> CreateAsync(CreateEmployeeAllowanceDto request);
    Task<Response<EmployeeAllowanceDto>> UpdateAsync(int employeeId, int allowanceId, UpdateEmployeeAllowanceDto request);
    Task<Response<bool>> DeleteAsync(int employeeId, int allowanceId);
}
