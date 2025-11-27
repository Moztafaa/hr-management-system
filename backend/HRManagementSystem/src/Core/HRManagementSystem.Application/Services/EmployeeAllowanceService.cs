namespace HRManagementSystem.Application.Services;

public class EmployeeAllowanceService(
        IUnitOfWork _unitOfWork,
        IMapper _mapper) : IEmployeeAllowanceService
{
    public async Task<Response<IEnumerable<EmployeeAllowanceDto>>> GetAllAsync()
    {
        try
        {
            var employeeAllowances = await _unitOfWork.EmployeeAllowances.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<EmployeeAllowanceDto>>(employeeAllowances);
            return new Response<IEnumerable<EmployeeAllowanceDto>>(dtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeAllowanceDto>>(null!, $"Error occurred while retrieving employee allowances: {ex.Message}", true);
        }
    }

    public async Task<Response<EmployeeAllowanceDto>> GetByCompositeKeyAsync(int employeeId, int allowanceId)
    {
        try
        {
            var employeeAllowance = await _unitOfWork.EmployeeAllowances.GetByCompositeKeyAsync(employeeId, allowanceId);
            if (employeeAllowance == null)
                return new Response<EmployeeAllowanceDto>(null!, "Employee allowance not found.", true);

            var dto = _mapper.Map<EmployeeAllowanceDto>(employeeAllowance);
            return new Response<EmployeeAllowanceDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeAllowanceDto>(null!, $"Error occurred while retrieving the employee allowance: {ex.Message}", true);
        }
    }
    public async Task<Response<IEnumerable<EmployeeAllowanceWithDetailsDto>>> GetByEmployeeIdAsync(int employeeId)
    {
        try
        {
            var allowances = await _unitOfWork.EmployeeAllowances.GetByEmployeeIdAsync(employeeId); //error here

            var dtos = _mapper.Map<IEnumerable<EmployeeAllowanceWithDetailsDto>>(allowances);

            return new Response<IEnumerable<EmployeeAllowanceWithDetailsDto>>(dtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeAllowanceWithDetailsDto>>(null!,
                $"Error occurred while retrieving allowances: {ex.Message}",
                true);
        }
    }



    public async Task<Response<EmployeeAllowanceDto>> CreateAsync(CreateEmployeeAllowanceDto request)
    {
        try
        {
            if (request.Recurrence == RecurrenceType.OneTime)
            {
                if (request.StartDate == null)
                    return new Response<EmployeeAllowanceDto>(null!, "Start date is required for one-time allowances.", true);

                request.EndDate = null;
            }
            if (request.Recurrence == RecurrenceType.Period)
            {
                if (request.StartDate == null || request.EndDate == null)
                    return new Response<EmployeeAllowanceDto>(null!, "Start and end dates are required for periodic allowances.", true);

                if (request.EndDate <= request.StartDate)
                    return new Response<EmployeeAllowanceDto>(null!, "End date must be greater than start date.", true);
            }

            if (request.Recurrence == RecurrenceType.Annual)
            {
                if (request.StartDate == null)
                    return new Response<EmployeeAllowanceDto>(null!, "Start date is required for annual allowances.", true);

                request.EndDate = null;
            }
            if (request.Recurrence == RecurrenceType.Permanent)
            {
                request.StartDate = null;
                request.EndDate = null;
            }


            var allowance = await _unitOfWork.Allowances.GetByIdAsync(request.AllowanceId);
            if (allowance == null)
                return new Response<EmployeeAllowanceDto>(null!, "Invalid allowance ID.", true);

            var existing = await _unitOfWork.EmployeeAllowances
                .GetByCompositeKeyAsync(request.EmployeeId, request.AllowanceId);

            if (existing != null)
                return new Response<EmployeeAllowanceDto>(null!, "Employee already has this allowance assigned.", true);

            var employeeAllowance = _mapper.Map<EmployeeAllowance>(request);

            
            employeeAllowance.Amount = request.Amount;
            employeeAllowance.IsPercentage = request.IsPercentage;

            employeeAllowance.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.EmployeeAllowances.AddAsync(employeeAllowance);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<EmployeeAllowanceDto>(employeeAllowance);
            return new Response<EmployeeAllowanceDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeAllowanceDto>(
                null!,
                $"Error occurred while creating the employee allowance: {ex.Message}",
                true
            );
        }
    }


    public async Task<Response<EmployeeAllowanceDto>> UpdateAsync(
        int employeeId,
        int allowanceId,
        UpdateEmployeeAllowanceDto request)
    {
        try
        {
            var employeeAllowance = await _unitOfWork.EmployeeAllowances
                .GetByCompositeKeyAsync(employeeId, allowanceId);

            if (employeeAllowance == null)
                return new Response<EmployeeAllowanceDto>(null!, "Employee allowance not found.", true);

            
            if (request.Recurrence.HasValue)
            {
                employeeAllowance.Recurrence = request.Recurrence.Value;

                switch (request.Recurrence.Value)
                {
                    case RecurrenceType.OneTime:
                        if (request.StartDate.HasValue)
                            employeeAllowance.StartDate = request.StartDate;
                        employeeAllowance.EndDate = null;
                        break;

                    case RecurrenceType.Period:
                        if (request.StartDate.HasValue && request.EndDate.HasValue)
                        {
                            if (request.EndDate <= request.StartDate)
                                return new Response<EmployeeAllowanceDto>(null!, "End date must be greater than start date.", true);

                            employeeAllowance.StartDate = request.StartDate;
                            employeeAllowance.EndDate = request.EndDate;
                        }
                        break;

                    case RecurrenceType.Permanent:
                        employeeAllowance.StartDate = null;
                        employeeAllowance.EndDate = null;
                        break;

                    case RecurrenceType.Annual:
                        if (!request.StartDate.HasValue)
                            return new Response<EmployeeAllowanceDto>(null!, "Start date is required for annual allowances.", true);

                        employeeAllowance.StartDate = request.StartDate;
                        employeeAllowance.EndDate = null; 
                        break;
                }
            }
            else
            {
                
                if (request.StartDate.HasValue)
                    employeeAllowance.StartDate = request.StartDate;
                if (request.EndDate.HasValue)
                    employeeAllowance.EndDate = request.EndDate;
            }

            
            if (request.Amount.HasValue)
                employeeAllowance.Amount = request.Amount.Value;
            if (request.IsPercentage.HasValue)
                employeeAllowance.IsPercentage = request.IsPercentage.Value;

           
            if (request.AllowanceId.HasValue && request.AllowanceId.Value != allowanceId)
            {
                var existing = await _unitOfWork.EmployeeAllowances
                    .GetByCompositeKeyAsync(employeeId, request.AllowanceId.Value);

                if (existing != null)
                    return new Response<EmployeeAllowanceDto>(null!, "Employee already has this allowance assigned.", true);

                employeeAllowance.AllowanceId = request.AllowanceId.Value;
            }

            employeeAllowance.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.EmployeeAllowances.UpdateAsync(employeeAllowance);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<EmployeeAllowanceDto>(employeeAllowance);
            return new Response<EmployeeAllowanceDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeAllowanceDto>(
                null!,
                $"Error occurred while updating the employee allowance: {ex.Message}",
                true
            );
        }
    }



    public async Task<Response<bool>> DeleteAsync(int employeeId, int allowanceId)
    {
        try
        {
            var employeeAllowance = await _unitOfWork.EmployeeAllowances.GetByCompositeKeyAsync(employeeId, allowanceId);
            if (employeeAllowance == null)
                return new Response<bool>(false, "Employee allowance not found.", true);

            await _unitOfWork.EmployeeAllowances.DeleteAsync(employeeAllowance.EmployeeId, employeeAllowance.AllowanceId);
            await _unitOfWork.SaveChangesAsync();

            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while deleting the employee allowance: {ex.Message}", true);
        }
    }
}
