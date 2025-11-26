namespace HRManagementSystem.Application.Services;

public class EmployeeDeductionService(IUnitOfWork _unitOfWork, IMapper _mapper) : IEmployeeDeductionService
{


    public async Task<Response<IEnumerable<EmployeeDeductionDto>>> GetAllAsync()
    {
        try
        {
            IEnumerable<EmployeeDeduction> employeeDeductions = await _unitOfWork.EmployeeDeductions.GetAllWithDetailsAsync();
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
            EmployeeDeduction? employeeDeduction = await _unitOfWork.EmployeeDeductions.GetByIdWithDetailsAsync(id);
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
            EmployeeDeduction? existingDeduction = await _unitOfWork.EmployeeDeductions.GetByIdAsync(id);
            if (existingDeduction == null)
                return new Response<EmployeeDeductionDto>(null!, "Employee deduction not found.", true);

            if (request.Amount.HasValue)
                existingDeduction.Amount = request.Amount.Value;

            if (request.Month.HasValue && request.Month.Value > 0)
                existingDeduction.Month = request.Month.Value;

            if (request.Year.HasValue && request.Year.Value > 0)
                existingDeduction.Year = request.Year.Value;

            existingDeduction.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.EmployeeDeductions.UpdateAsync(existingDeduction);
            await _unitOfWork.SaveChangesAsync();

            EmployeeDeductionDto dto = _mapper.Map<EmployeeDeductionDto>(existingDeduction);
            return new Response<EmployeeDeductionDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDeductionDto>(null!,
                $"Error occurred while updating the employee deduction: {ex.Message}",
                true);
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

    public async Task<Response<IEnumerable<EmployeeDeductionDto>>> GetByEmployeeAndMonthAsync(int employeeId, int month, int year)
    {
        try
        {
            IEnumerable<EmployeeDeduction> employeeDeductions = await _unitOfWork.EmployeeDeductions
                .GetByEmployeeAndMonthAsync(employeeId, month, year);

            IEnumerable<EmployeeDeductionDto> dtoList = _mapper.Map<IEnumerable<EmployeeDeductionDto>>(employeeDeductions);

            foreach (EmployeeDeductionDto dto in dtoList)
            {
                dto.DeductionName = employeeDeductions
                    .First(ed => ed.Id == dto.Id)
                    .Deduction?.Name ?? string.Empty;
            }

            return new Response<IEnumerable<EmployeeDeductionDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeDeductionDto>>(null!,
                $"Error occurred while retrieving employee deductions: {ex.Message}", true);
        }
    }

    //public async Task<Response<decimal>> GetTotalDeductionsByEmployeeAndMonthAsync(int employeeId, int month, int year)
    //{
    //    try
    //    {
    //        decimal total = await _unitOfWork.EmployeeDeductions
    //            .GetTotalDeductionsByEmployeeAndMonthAsync(employeeId, month, year);

    //        return new Response<decimal>(total, string.Empty, false);
    //    }
    //    catch (Exception ex)
    //    {
    //        return new Response<decimal>(0, $"Error occurred while calculating total deductions: {ex.Message}", true);
    //    }
    //}

}
