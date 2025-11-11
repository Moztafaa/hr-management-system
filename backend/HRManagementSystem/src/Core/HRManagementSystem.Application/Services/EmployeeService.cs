namespace HRManagementSystem.Application.Services;

public class EmployeeService(IRepository<Employee> employeeRepository, IMapper _mapper) : IEmployeeService
{
    public async Task<Response<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken = default)
    {
        try
        {
            Employee? employee = _mapper.Map<Employee>(createEmployeeDto);

            await employeeRepository.AddAsync(employee, cancellationToken);

            EmployeeDto? dto = _mapper.Map<EmployeeDto>(employee);

            return new Response<EmployeeDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDto>(default, ex.Message, true);
        }
    }

    public async Task<Response<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Employee> employees = await employeeRepository.GetAllAsync(cancellationToken);

            IEnumerable<EmployeeDto> dtoList = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return new Response<IEnumerable<EmployeeDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeDto>>(null, ex.Message, true);
        }
    }

    public async Task<Response<EmployeeDto>> GetEmployeeByIdAsync(int id, CancellationToken cancellationToken = default)

    {
        try
        {
            Employee? employee = await employeeRepository.GetByIdAsync(id, cancellationToken);
            if (employee is null)
                return new Response<EmployeeDto>(null, "Employee not found", true);

            EmployeeDto? dto = _mapper.Map<EmployeeDto>(employee);

            return new Response<EmployeeDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDto>(null, ex.Message, true);
        }
    }
    public async Task<Response<bool>> UpdateEmployeeAsync(int id, UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken = default)

    {
        try
        {
            Employee? employee = await employeeRepository.GetByIdAsync(id, cancellationToken);
            if (employee is null)
                return new Response<bool>(false, "Employee not found", true);

            _mapper.Map(updateEmployeeDto, employee);

            await employeeRepository.UpdateAsync(employee, cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, true);
        }
    }
    public async Task<Response<bool>> DeleteEmployeeAsync(int id, CancellationToken cancellationToken = default)

    {
        try
        {
            Employee? employee = await employeeRepository.GetByIdAsync(id, cancellationToken);
            if (employee is null)
                return new Response<bool>(false, "Employee not found", true);

            await employeeRepository.DeleteAsync(id, cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, true);
        }
    }

}
