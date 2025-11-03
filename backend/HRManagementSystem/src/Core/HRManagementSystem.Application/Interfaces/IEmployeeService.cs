namespace HRManagementSystem.Application.Interfaces;

public interface IEmployeeService
{

    Task<Response<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync(CancellationToken cancellationToken = default);
    Task<Response<EmployeeDto>> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Response<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken = default);

}
