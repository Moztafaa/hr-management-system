namespace HRManagementSystem.Application.Interfaces;

public interface IEmployeeService
{

    Task<Response<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync(CancellationToken cancellationToken = default);
    Task<Response<EmployeeDto>> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> UpdateEmployeeAsync(int id, UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default);

}
