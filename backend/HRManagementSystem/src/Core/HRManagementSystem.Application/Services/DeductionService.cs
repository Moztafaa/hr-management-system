namespace HRManagementSystem.Application.Services;

public class DeductionService(IUnitOfWork _unitOfWork, IMapper _mapper) : IDeductionService
{
    public async Task<Response<IEnumerable<DeductionDto>>> GetAllAsync()
    {
        try
        {
            IEnumerable<Deduction> deductions = await _unitOfWork.Deductions.GetAllAsync();
            IEnumerable<DeductionDto> deductionDtos = _mapper.Map<IEnumerable<DeductionDto>>(deductions);

            return new Response<IEnumerable<DeductionDto>>(deductionDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<DeductionDto>>(null!, $"Error occurred while retrieving deductions: {ex.Message}", true);
        }
    }

    public async Task<Response<DeductionDto>> GetByIdAsync(int id)
    {
        try
        {
            Deduction? deduction = await _unitOfWork.Deductions.GetByIdAsync(id);
            if (deduction == null)
                return new Response<DeductionDto>(null!, "Deduction not found.", true);

            DeductionDto deductionDto = _mapper.Map<DeductionDto>(deduction);
            return new Response<DeductionDto>(deductionDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<DeductionDto>(null!, $"Error occurred while retrieving the deduction: {ex.Message}", true);
        }
    }

    public async Task<Response<DeductionDto>> CreateAsync(CreateDeductionDto request)
    {
        try
        {
            Deduction? existing = await _unitOfWork.Deductions.GetByNameAsync(request.Name);
            if (existing != null)
                return new Response<DeductionDto>(default!, "Deduction with the same name already exists.", true);

            Deduction deduction = _mapper.Map<Deduction>(request);
            deduction.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Deductions.AddAsync(deduction);
            await _unitOfWork.SaveChangesAsync();

            DeductionDto createdDto = _mapper.Map<DeductionDto>(deduction);
            return new Response<DeductionDto>(createdDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<DeductionDto>(default!, $"Error occurred while creating the deduction: {ex.Message}", true);
        }
    }

    public async Task<Response<DeductionDto>> UpdateAsync(int id, UpdateDeductionDto request)
    {
        try
        {
            Deduction? deduction = await _unitOfWork.Deductions.GetByIdAsync(id);
            if (deduction == null)
                return new Response<DeductionDto>(default!, "Deduction not found.", true);


            // Only check for name conflict if Name is provided
            if (!string.IsNullOrEmpty(request.Name) &&
                !string.Equals(deduction.Name, request.Name, StringComparison.OrdinalIgnoreCase))
            {
                Deduction? existing = await _unitOfWork.Deductions.GetByNameAsync(request.Name);
                if (existing != null && existing.Id != id)
                    return new Response<DeductionDto>(default!, "Another deduction with this name already exists.", true);
            }


            // Apply updates only if values are provided
            if (!string.IsNullOrEmpty(request.Name))
                deduction.Name = request.Name;
            if (request.Amount.HasValue)
                deduction.Amount = request.Amount.Value;
            if (request.IsPercentage.HasValue)
                deduction.IsPercentage = request.IsPercentage.Value;

            deduction.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Deductions.UpdateAsync(deduction);
            await _unitOfWork.SaveChangesAsync();

            DeductionDto updatedDto = _mapper.Map<DeductionDto>(deduction);
            return new Response<DeductionDto>(updatedDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<DeductionDto>(default!, $"Error occurred while updating the deduction: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            Deduction? deduction = await _unitOfWork.Deductions.GetByIdAsync(id);
            if (deduction == null)
                return new Response<bool>(false, "Deduction not found.", true);

            await _unitOfWork.Deductions.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while deleting the deduction: {ex.Message}", true);
        }
    }
}
