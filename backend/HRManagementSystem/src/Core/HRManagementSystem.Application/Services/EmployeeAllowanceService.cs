namespace HRManagementSystem.Application.Services;

public class EmployeeAllowanceService(
        IUnitOfWork _unitOfWork,
        IMapper _mapper) : IEmployeeAllowanceService
{
    public async Task<Response<IEnumerable<EmployeeAllowanceDto>>> GetAllAsync()
    {
        try
        {
            IEnumerable<EmployeeAllowance> employeeAllowances = await _unitOfWork.EmployeeAllowances.GetAllWithDetailsAsync();
            IEnumerable<EmployeeAllowanceDto> employeeAllowanceDtos = _mapper.Map<IEnumerable<EmployeeAllowanceDto>>(employeeAllowances);
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
            EmployeeAllowance? employeeAllowance = await _unitOfWork.EmployeeAllowances.GetByIdWithDetailsAsync(id);
            if (employeeAllowance == null)
                return new Response<EmployeeAllowanceDto>(default!, "Employee allowance not found.", true);

            EmployeeAllowanceDto dto = _mapper.Map<EmployeeAllowanceDto>(employeeAllowance);
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
            Allowance? allowance = await _unitOfWork.Allowances.GetByIdAsync(request.AllowanceId);
            if (allowance == null)
                return new Response<EmployeeAllowanceDto>(default!, "Invalid allowance ID.", true);

            IEnumerable<EmployeeAllowance> existingAllowances = await _unitOfWork.EmployeeAllowances.GetByEmployeeAndMonthAsync(request.EmployeeId,request.Month,request.Year);
            if (existingAllowances.Any(a => a.AllowanceId == request.AllowanceId))
                return new Response<EmployeeAllowanceDto>(default!, "Employee already has this allowance assigned.", true);

            EmployeeAllowance employeeAllowance = _mapper.Map<EmployeeAllowance>(request);
            employeeAllowance.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.EmployeeAllowances.AddAsync(employeeAllowance);
            await _unitOfWork.SaveChangesAsync();
            EmployeeAllowanceDto dto = _mapper.Map<EmployeeAllowanceDto>(employeeAllowance);

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
            EmployeeAllowance? employeeAllowance = await _unitOfWork.EmployeeAllowances.GetByIdAsync(id);
            if (employeeAllowance == null)
                return new Response<EmployeeAllowanceDto>(default!, "Employee allowance not found.", true);

            if (request.Amount.HasValue)
                employeeAllowance.Amount = request.Amount.Value;

            if (request.Month.HasValue && request.Month.Value > 0)
                employeeAllowance.Month = request.Month.Value;

            if (request.Year.HasValue && request.Year.Value > 0)
                employeeAllowance.Year = request.Year.Value;

            employeeAllowance.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.EmployeeAllowances.UpdateAsync(employeeAllowance);
            await _unitOfWork.SaveChangesAsync();

            EmployeeAllowanceDto dto = _mapper.Map<EmployeeAllowanceDto>(employeeAllowance);

            return new Response<EmployeeAllowanceDto>(dto, "", false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeAllowanceDto>(
                default!,
                $"Error occurred while updating employee allowance: {ex.Message}",
                true
            );
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            EmployeeAllowance? employeeAllowance = await _unitOfWork.EmployeeAllowances.GetByIdAsync(id);
            if (employeeAllowance == null)
                return new Response<bool>(false, "Employee allowance not found.", true);

            await _unitOfWork.EmployeeAllowances.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while deleting the employee allowance: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<EmployeeAllowanceDto>>>GetByEmployeeAndMonthAsync(int employeeId, int month, int year)
    {
        try
        {
            IEnumerable<EmployeeAllowance> employeeAllowances = await _unitOfWork.EmployeeAllowances.GetByEmployeeAndMonthAsync(employeeId, month, year);

            IEnumerable<EmployeeAllowanceDto> dtoList = _mapper.Map<IEnumerable<EmployeeAllowanceDto>>(employeeAllowances);

            foreach (EmployeeAllowanceDto dto in dtoList)
            {
                dto.AllowanceName = employeeAllowances
                    .First(ea => ea.Id == dto.Id)
                    .Allowance?.Name ?? string.Empty;
            }

            return new Response<IEnumerable<EmployeeAllowanceDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeAllowanceDto>>(null!,
                $"Error occurred while retrieving employee allowances: {ex.Message}", true);
        }
    }

    //public async Task<Response<decimal>>GetTotalAllowancesByEmployeeAndMonthAsync(int employeeId, int month, int year)
    //{
    //    try
    //    {
    //        decimal total = await _unitOfWork.EmployeeAllowances
    //            .GetTotalAllowancesByEmployeeAndMonthAsync(employeeId, month, year);

    //        return new Response<decimal>(total, string.Empty, false);
    //    }
    //    catch (Exception ex)
    //    {
    //        return new Response<decimal>(0, $"Error occurred while calculating total allowances: {ex.Message}", true);
    //    }
    //}


}
