namespace HRManagementSystem.Application.Services;

public class EmployeeDeductionService(IRepository<EmployeeDeduction> _employeeDeductionRepository, IMapper _mapper) : IEmployeeDeductionService
{


    public async Task<Response<IEnumerable<EmployeeDeductionDto>>> GetAllAsync()
    {
        try
        {
            var employeeDeductions = await _employeeDeductionRepository.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<EmployeeDeductionDto>>(employeeDeductions);
            return new Response<IEnumerable<EmployeeDeductionDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeDeductionDto>>(null, ex.Message, true);
        }
    }

    public async Task<Response<EmployeeDeductionDto>> GetByIdAsync(int id)
    {
        try
        {
            var employeeDeduction = await _employeeDeductionRepository.GetByIdAsync(id);
            if (employeeDeduction == null)
                return new Response<EmployeeDeductionDto>(null, "Employee deduction not found", true);

            var dto = _mapper.Map<EmployeeDeductionDto>(employeeDeduction);
            return new Response<EmployeeDeductionDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDeductionDto>(null, ex.Message, true);
        }
    }

    public async Task<Response<EmployeeDeductionDto>> CreateAsync(CreateEmployeeDeductionDto request)
    {
        try
        {
            var entity = _mapper.Map<EmployeeDeduction>(request);
            await _employeeDeductionRepository.AddAsync(entity);

            var dto = _mapper.Map<EmployeeDeductionDto>(entity);
            return new Response<EmployeeDeductionDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDeductionDto>(null, ex.Message, true);
        }
    }

    public async Task<Response<EmployeeDeductionDto>> UpdateAsync(int id, UpdateEmployeeDeductionDto request)
    {
        try
        {
            var existing = await _employeeDeductionRepository.GetByIdAsync(id);
            if (existing == null)
                return new Response<EmployeeDeductionDto>(null, "Employee deduction not found", true);

            _mapper.Map(request, existing);
            await _employeeDeductionRepository.UpdateAsync(existing);

            var dto = _mapper.Map<EmployeeDeductionDto>(existing);
            return new Response<EmployeeDeductionDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDeductionDto>(null, ex.Message, true);
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            var existing = await _employeeDeductionRepository.GetByIdAsync(id);
            if (existing == null)
                return new Response<bool>(false, "Employee deduction not found", true);

            await _employeeDeductionRepository.DeleteAsync(existing.Id);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, true);
        }
    }
}
