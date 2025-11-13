namespace HRManagementSystem.Application.Services;

public class DeductionService(IDeductionRepository _deductionRepository, IMapper _mapper) : IDeductionService
{
    public async Task<Response<IEnumerable<DeductionDto>>> GetAllAsync()
    {
        try
        {
            var deductions = await _deductionRepository.GetAllAsync();
            var deductionDtos = _mapper.Map<IEnumerable<DeductionDto>>(deductions);

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
            var deduction = await _deductionRepository.GetByIdAsync(id);
            if (deduction == null)
                return new Response<DeductionDto>(null!, "Deduction not found.", true);

            var deductionDto = _mapper.Map<DeductionDto>(deduction);
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
            var existing = await _deductionRepository.GetByNameAsync(request.Name);
            if (existing != null)
                return new Response<DeductionDto>(default!, "Deduction with the same name already exists.", true);

            var deduction = _mapper.Map<Deduction>(request);
            deduction.CreatedAt = DateTime.UtcNow;

            await _deductionRepository.AddAsync(deduction);

            var createdDto = _mapper.Map<DeductionDto>(deduction);
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
            var deduction = await _deductionRepository.GetByIdAsync(id);
            if (deduction == null)
                return new Response<DeductionDto>(default!, "Deduction not found.", true);

 
            if (!string.Equals(deduction.Name, request.Name, StringComparison.OrdinalIgnoreCase))
            {
                var existing = await _deductionRepository.GetByNameAsync(request.Name);
                if (existing != null && existing.Id != id)
                    return new Response<DeductionDto>(default!, "Another deduction with this name already exists.", true);
            }

            deduction.Name = request.Name;
            deduction.Amount = request.Amount;
            deduction.UpdatedAt = DateTime.UtcNow;

            await _deductionRepository.UpdateAsync(deduction);

            var updatedDto = _mapper.Map<DeductionDto>(deduction);
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
            var deduction = await _deductionRepository.GetByIdAsync(id);
            if (deduction == null)
                return new Response<bool>(false, "Deduction not found.", true);

            await _deductionRepository.DeleteAsync(id);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while deleting the deduction: {ex.Message}", true);
        }
    }
}
