namespace HRManagementSystem.Application.Services;

public class EmployeeDeductionService(
        IUnitOfWork _unitOfWork,
        IMapper _mapper) : IEmployeeDeductionService
{
    public async Task<Response<IEnumerable<EmployeeDeductionDto>>> GetAllAsync()
    {
        try
        {
            var employeeDeductions = await _unitOfWork.EmployeeDeductions.GetAllAsync();
            var dtos = _mapper.Map<IEnumerable<EmployeeDeductionDto>>(employeeDeductions);
            return new Response<IEnumerable<EmployeeDeductionDto>>(dtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeDeductionDto>>(null!, ex.Message, true);
        }
    }

    public async Task<Response<EmployeeDeductionDto>> GetByCompositeKeyAsync(int employeeId, int deductionId)
    {
        try
        {
            var employeeDeduction = await _unitOfWork.EmployeeDeductions.GetByCompositeKeyAsync(employeeId, deductionId);
            if (employeeDeduction == null)
                return new Response<EmployeeDeductionDto>(null!, "Employee deduction not found", true);

            var dto = _mapper.Map<EmployeeDeductionDto>(employeeDeduction);
            return new Response<EmployeeDeductionDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDeductionDto>(null!, ex.Message, true);
        }
    }
    public async Task<Response<IEnumerable<EmployeeDeductionWithDetailsDto>>> GetByEmployeeIdAsync(int employeeId)
    {
        try
        {
            
            var deductions = await _unitOfWork.EmployeeDeductions.GetByEmployeeIdAsync(employeeId);

           
            var dtos = _mapper.Map<IEnumerable<EmployeeDeductionWithDetailsDto>>(deductions);

            return new Response<IEnumerable<EmployeeDeductionWithDetailsDto>>(dtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<EmployeeDeductionWithDetailsDto>>(
                null!,
                $"Error occurred while retrieving deductions for employee: {ex.Message}",
                true
            );
        }
    }


    public async Task<Response<EmployeeDeductionDto>> CreateAsync(CreateEmployeeDeductionDto request)
    {
        try
        {
            if (request.Recurrence == RecurrenceType.OneTime && request.StartDate == null)
                return new Response<EmployeeDeductionDto>(null!, "Start date is required for one-time deductions.", true);

            if (request.Recurrence == RecurrenceType.Period)
            {
                if (request.StartDate == null || request.EndDate == null)
                    return new Response<EmployeeDeductionDto>(null!, "Start and end dates are required for periodic deductions.", true);

                if (request.EndDate <= request.StartDate)
                    return new Response<EmployeeDeductionDto>(null!, "End date must be greater than start date.", true);
            }
            if (request.Recurrence == RecurrenceType.Annual)
            {
                if (request.StartDate == null)
                    return new Response<EmployeeDeductionDto>(null!, "Start date is required for annual deduction.", true);
                request.EndDate = null;
            }
            if (request.Recurrence == RecurrenceType.Permanent)
            {
                request.StartDate = null;
                request.EndDate = null;
            }

            var existing = await _unitOfWork.EmployeeDeductions.GetByCompositeKeyAsync(request.EmployeeId, request.DeductionId);
            if (existing != null)
                return new Response<EmployeeDeductionDto>(null!, "Employee already has this deduction assigned.", true);

            var entity = _mapper.Map<EmployeeDeduction>(request);
            entity.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.EmployeeDeductions.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<EmployeeDeductionDto>(entity);
            return new Response<EmployeeDeductionDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDeductionDto>(null!, ex.Message, true);
        }
    }


    public async Task<Response<EmployeeDeductionDto>> UpdateAsync(
        int employeeId,
        int deductionId,
        UpdateEmployeeDeductionDto request)
    {
        try
        {
            var employeeDeduction = await _unitOfWork.EmployeeDeductions
                .GetByCompositeKeyAsync(employeeId, deductionId);

            if (employeeDeduction == null)
                return new Response<EmployeeDeductionDto>(null!, "Employee deduction not found", true);

            if (request.Recurrence.HasValue)
            {
                employeeDeduction.Recurrence = request.Recurrence.Value;

                switch (request.Recurrence.Value)
                {
                    case RecurrenceType.OneTime:
                        
                        if (request.StartDate.HasValue)
                        {
                            employeeDeduction.StartDate = request.StartDate;
                        }
                        employeeDeduction.EndDate = null;
                        break;

                    case RecurrenceType.Period:
          
                        if (request.StartDate.HasValue && request.EndDate.HasValue)
                        {
                            if (request.EndDate <= request.StartDate)
                                return new Response<EmployeeDeductionDto>(null!, "End date must be greater than start date.", true);

                            employeeDeduction.StartDate = request.StartDate;
                            employeeDeduction.EndDate = request.EndDate;
                        }
                        break;

                    case RecurrenceType.Annual:
                        if (!request.StartDate.HasValue)
                            return new Response<EmployeeDeductionDto>(null!, "Start date is required for annual deductions.", true);

                        employeeDeduction.StartDate = request.StartDate;
                        employeeDeduction.EndDate = null; 
                        break;

                    case RecurrenceType.Permanent:
                        employeeDeduction.StartDate = null;
                        employeeDeduction.EndDate = null;
                        break;
                }
            }
            else
            {
                
                if (request.StartDate.HasValue)
                    employeeDeduction.StartDate = request.StartDate;
                if (request.EndDate.HasValue)
                    employeeDeduction.EndDate = request.EndDate;
            }


            if (request.Amount.HasValue)
                employeeDeduction.Amount = request.Amount.Value;

            if (request.IsPercentage.HasValue)
                employeeDeduction.IsPercentage = request.IsPercentage.Value;

            if (request.DeductionId.HasValue && request.DeductionId.Value != deductionId)
            {
                var duplicate = await _unitOfWork.EmployeeDeductions
                    .GetByCompositeKeyAsync(employeeId, request.DeductionId.Value);

                if (duplicate != null)
                    return new Response<EmployeeDeductionDto>(null!, "Employee already has this deduction assigned.", true);

                employeeDeduction.DeductionId = request.DeductionId.Value;
            }


            employeeDeduction.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.EmployeeDeductions.UpdateAsync(employeeDeduction);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<EmployeeDeductionDto>(employeeDeduction);
            return new Response<EmployeeDeductionDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<EmployeeDeductionDto>(null!, ex.Message, true);
        }
    }


    public async Task<Response<bool>> DeleteAsync(int employeeId, int deductionId)
    {
        try
        {
            var employeeDeduction = await _unitOfWork.EmployeeDeductions.GetByCompositeKeyAsync(employeeId, deductionId);
            if (employeeDeduction == null)
                return new Response<bool>(false, "Employee deduction not found", true);

            await _unitOfWork.EmployeeDeductions.DeleteAsync(employeeDeduction.EmployeeId, employeeDeduction.DeductionId);
            await _unitOfWork.SaveChangesAsync();

            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, ex.Message, true);
        }
    }
}
