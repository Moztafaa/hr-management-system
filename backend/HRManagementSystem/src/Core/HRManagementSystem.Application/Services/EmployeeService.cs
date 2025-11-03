namespace HRManagementSystem.Application.Services;

public class EmployeeService(IRepository<Employee> employeeRepository) : IEmployeeService
{
    public async Task<Response<EmployeeDto>> CreateEmployeeAsync(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var employee = new Employee
            {
                FirstName = createEmployeeDto.FirstName,
                LastName = createEmployeeDto.LastName,
                DateOfBirth = createEmployeeDto.DateOfBirth,
                Gender = createEmployeeDto.Gender,
                HireDate = createEmployeeDto.HireDate,
                EFF_Start = createEmployeeDto.EFF_Start,
                EFF_End = createEmployeeDto.EFF_End,
                Email = createEmployeeDto.Email,
                ContactNumber = createEmployeeDto.ContactNumber,
                Address = createEmployeeDto.Address,
                BasicSalary = createEmployeeDto.BasicSalary,
                ApplicationUserId = createEmployeeDto.ApplicationUserId
            };

            await employeeRepository.AddAsync(employee, cancellationToken);

            var dto = new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
                HireDate = employee.HireDate,
                EFF_Start = employee.EFF_Start,
                EFF_End = employee.EFF_End,
                Email = employee.Email,
                ContactNumber = employee.ContactNumber,
                Address = employee.Address,
                BasicSalary = employee.BasicSalary,
                ApplicationUserId = employee.ApplicationUserId
            };

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
            var employees = await employeeRepository.GetAllAsync(cancellationToken);

            var dtoList = employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                DateOfBirth = e.DateOfBirth,
                Gender = e.Gender,
                HireDate = e.HireDate,
                EFF_Start = e.EFF_Start,
                EFF_End = e.EFF_End,
                Email = e.Email,
                ContactNumber = e.ContactNumber,
                Address = e.Address,
                BasicSalary = e.BasicSalary,
                ApplicationUserId = e.ApplicationUserId
            });

            return new Response<IEnumerable<EmployeeDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeDto>>(null, ex.Message, true);
        }
    }

    public async Task<Response<EmployeeDto>> GetEmployeeByIdAsync(Guid id, CancellationToken cancellationToken = default)

    {
        try
        {
            var employee = await employeeRepository.GetByIdAsync(id, cancellationToken);
            if (employee is null)
                return new Response<EmployeeDto>(null, "Employee not found", true);

            var dto = new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DateOfBirth = employee.DateOfBirth,
                Gender = employee.Gender,
                HireDate = employee.HireDate,
                EFF_Start = employee.EFF_Start,
                EFF_End = employee.EFF_End,
                Email = employee.Email,
                ContactNumber = employee.ContactNumber,
                Address = employee.Address,
                BasicSalary = employee.BasicSalary,
                ApplicationUserId = employee.ApplicationUserId
            };

            return new Response<EmployeeDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDto>(null, ex.Message, true);
        }
    }
    public async Task<Response<bool>> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken = default)

    {
        try
        {
            var employee = await employeeRepository.GetByIdAsync(id, cancellationToken);
            if (employee is null)
                return new Response<bool>(false, "Employee not found", true);

            employee.FirstName = updateEmployeeDto.FirstName;
            employee.LastName = updateEmployeeDto.LastName;
            employee.DateOfBirth = updateEmployeeDto.DateOfBirth;
            employee.Gender = updateEmployeeDto.Gender;
            employee.HireDate = updateEmployeeDto.HireDate;
            employee.EFF_Start = updateEmployeeDto.EFF_Start;
            employee.EFF_End = updateEmployeeDto.EFF_End;
            employee.Email = updateEmployeeDto.Email;
            employee.ContactNumber = updateEmployeeDto.ContactNumber;
            employee.Address = updateEmployeeDto.Address;
            employee.BasicSalary = updateEmployeeDto.BasicSalary;
            employee.ApplicationUserId = updateEmployeeDto.ApplicationUserId;

            await employeeRepository.UpdateAsync(employee, cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, true);
        }
    }
    public async Task<Response<bool>> DeleteEmployeeAsync(Guid id, CancellationToken cancellationToken = default)

    {
        try
        {
            var employee = await employeeRepository.GetByIdAsync(id, cancellationToken);
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
