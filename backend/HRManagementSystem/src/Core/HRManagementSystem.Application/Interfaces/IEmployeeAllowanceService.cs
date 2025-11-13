namespace HRManagementSystem.Application.Interfaces;

public interface IEmployeeAllowanceService
{
    Task<Response<IEnumerable<EmployeeAllowanceDto>>> GetAllAsync();
    Task<Response<EmployeeAllowanceDto>> GetByIdAsync(int id);
    Task<Response<EmployeeAllowanceDto>> CreateAsync(CreateEmployeeAllowanceDto request);
    Task<Response<EmployeeAllowanceDto>> UpdateAsync(int id, UpdateEmployeeAllowanceDto request);
    Task<Response<bool>> DeleteAsync(int id);
}
