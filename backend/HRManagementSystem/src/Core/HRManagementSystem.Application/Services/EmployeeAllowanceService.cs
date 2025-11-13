namespace HRManagementSystem.Application.Services;

public class EmployeeAllowanceService(
        IEmployeeAllowanceRepository _employeeAllowanceRepository,
        IAllowanceRepository _allowanceRepository,
        IMapper _mapper) : IEmployeeAllowanceService
{
    public async Task<Response<IEnumerable<EmployeeAllowanceDto>>> GetAllAsync()
    {
        try
        {
            var employeeAllowances = await _employeeAllowanceRepository.GetAllAsync();
            var employeeAllowanceDtos = _mapper.Map<IEnumerable<EmployeeAllowanceDto>>(employeeAllowances);
            return new Response<IEnumerable<EmployeeAllowanceDto>>(employeeAllowanceDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeAllowanceDto>>(null!, $"Error occurred while retrieving employee allowances: {ex.Message}", true);
        }
    }

    public async Task<Response<EmployeeAllowanceDto>> GetByIdAsync(int id)
    {
        try
        {
            var employeeAllowance = await _employeeAllowanceRepository.GetByIdAsync(id);
            if (employeeAllowance == null)
                return new Response<EmployeeAllowanceDto>(default!, "Employee allowance not found.", true);

            var dto = _mapper.Map<EmployeeAllowanceDto>(employeeAllowance);
            return new Response<EmployeeAllowanceDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeAllowanceDto>(default!, $"Error occurred while retrieving the employee allowance: {ex.Message}", true);
        }
    }

    public async Task<Response<EmployeeAllowanceDto>> CreateAsync(CreateEmployeeAllowanceDto request)
    {
        try
        {
            var allowance = await _allowanceRepository.GetByIdAsync(request.AllowanceId);
            if (allowance == null)
                return new Response<EmployeeAllowanceDto>(default!, "Invalid allowance ID.", true);

            var existingAllowances = await _employeeAllowanceRepository.GetByEmployeeIdAsync(request.EmployeeId);
            if (existingAllowances.Any(a => a.AllowanceId == request.AllowanceId))
                return new Response<EmployeeAllowanceDto>(default!, "Employee already has this allowance assigned.", true);

            var employeeAllowance = _mapper.Map<EmployeeAllowance>(request);
            employeeAllowance.CreatedAt = DateTime.UtcNow;

            await _employeeAllowanceRepository.AddAsync(employeeAllowance);
            var dto = _mapper.Map<EmployeeAllowanceDto>(employeeAllowance);

            return new Response<EmployeeAllowanceDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeAllowanceDto>(default!, $"Error occurred while creating the employee allowance: {ex.Message}", true);
        }
    }

    public async Task<Response<EmployeeAllowanceDto>> UpdateAsync(int id, UpdateEmployeeAllowanceDto request)
    {
        try
        {
            var employeeAllowance = await _employeeAllowanceRepository.GetByIdAsync(id);
            if (employeeAllowance == null)
                return new Response<EmployeeAllowanceDto>(default!, "Employee allowance not found.", true);


            var allowance = await _allowanceRepository.GetByIdAsync(request.AllowanceId);
            if (allowance == null)
                return new Response<EmployeeAllowanceDto>(default!, "Invalid allowance ID.", true);

            employeeAllowance.AllowanceId = request.AllowanceId;
            employeeAllowance.EmployeeId = request.EmployeeId;
            employeeAllowance.Amount = request.Amount;
            employeeAllowance.UpdatedAt = DateTime.UtcNow;

            await _employeeAllowanceRepository.UpdateAsync(employeeAllowance);

            var dto = _mapper.Map<EmployeeAllowanceDto>(employeeAllowance);
            return new Response<EmployeeAllowanceDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeAllowanceDto>(default!, $"Error occurred while updating the employee allowance: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            var employeeAllowance = await _employeeAllowanceRepository.GetByIdAsync(id);
            if (employeeAllowance == null)
                return new Response<bool>(false, "Employee allowance not found.", true);

            await _employeeAllowanceRepository.DeleteAsync(id);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while deleting the employee allowance: {ex.Message}", true);
        }
    }
}
