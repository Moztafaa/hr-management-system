namespace HRManagementSystem.Application.Services;

public class EmployeeDeductionService(IUnitOfWork _unitOfWork, IMapper _mapper) : IEmployeeDeductionService
{


    public async Task<Response<IEnumerable<EmployeeDeductionDto>>> GetAllAsync()
    {
        try
        {
            IEnumerable<EmployeeDeduction> employeeDeductions = await _unitOfWork.EmployeeDeductions.GetAllAsync();
            IEnumerable<EmployeeDeductionDto> dtoList = _mapper.Map<IEnumerable<EmployeeDeductionDto>>(employeeDeductions);
            return new Response<IEnumerable<EmployeeDeductionDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeDeductionDto>>(null!, ex.Message, true);
        }
    }

    public async Task<Response<EmployeeDeductionDto>> GetByIdAsync(int id)
    {
        try
        {
            EmployeeDeduction? employeeDeduction = await _unitOfWork.EmployeeDeductions.GetByIdAsync(id);
            if (employeeDeduction == null)
                return new Response<EmployeeDeductionDto>(null!, "Employee deduction not found", true);

            EmployeeDeductionDto dto = _mapper.Map<EmployeeDeductionDto>(employeeDeduction);
            return new Response<EmployeeDeductionDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDeductionDto>(null!, ex.Message, true);
        }
    }

    public async Task<Response<EmployeeDeductionDto>> CreateAsync(CreateEmployeeDeductionDto request)
    {
        try
        {
            EmployeeDeduction entity = _mapper.Map<EmployeeDeduction>(request);
            await _unitOfWork.EmployeeDeductions.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            EmployeeDeductionDto dto = _mapper.Map<EmployeeDeductionDto>(entity);
            return new Response<EmployeeDeductionDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDeductionDto>(null!, ex.Message, true);
        }
    }

    public async Task<Response<EmployeeDeductionDto>> UpdateAsync(int id, UpdateEmployeeDeductionDto request)
    {
        try
        {
            EmployeeDeduction? existing = await _unitOfWork.EmployeeDeductions.GetByIdAsync(id);
            if (existing == null)
                return new Response<EmployeeDeductionDto>(null!, "Employee deduction not found", true);

            _mapper.Map(request, existing);
            await _unitOfWork.EmployeeDeductions.UpdateAsync(existing);
            await _unitOfWork.SaveChangesAsync();

            EmployeeDeductionDto dto = _mapper.Map<EmployeeDeductionDto>(existing);
            return new Response<EmployeeDeductionDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDeductionDto>(null!, ex.Message, true);
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            EmployeeDeduction? existing = await _unitOfWork.EmployeeDeductions.GetByIdAsync(id);
            if (existing == null)
                return new Response<bool>(false, "Employee deduction not found", true);

            await _unitOfWork.EmployeeDeductions.DeleteAsync(existing.Id);
            await _unitOfWork.SaveChangesAsync();
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, true);
        }
    }
}
