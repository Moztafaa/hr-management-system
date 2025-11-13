namespace HRManagementSystem.Application.Services;

public class AllowanceService(IAllowanceRepository _allowanceRepository, IMapper _mapper) : IAllowanceService
{


    public async Task<Response<IEnumerable<AllowanceDto>>> GetAllAsync()
    {
        try
        {
            var allowances = await _allowanceRepository.GetAllAsync();
            var allowanceDtos = _mapper.Map<IEnumerable<AllowanceDto>>(allowances);

            return new Response<IEnumerable<AllowanceDto>>(allowanceDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<AllowanceDto>>(null!, $"Error occurred while retrieving allowances: {ex.Message}", true);
        }
    }

    public async Task<Response<AllowanceDto>> GetByIdAsync(int id)
    {
        try
        {
            var allowance = await _allowanceRepository.GetByIdAsync(id);
            if (allowance == null)
                return new Response<AllowanceDto>(null!, "Allowance not found.", true);

            var allowanceDto = _mapper.Map<AllowanceDto>(allowance);
            return new Response<AllowanceDto>(allowanceDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<AllowanceDto>(null!, $"Error occurred while retrieving the allowance: {ex.Message}", true);
        }
    }

    public async Task<Response<AllowanceDto>> CreateAsync(CreateAllowanceDto request)
    {
        try
        {
            var existing = await _allowanceRepository.GetByNameAsync(request.Name);
            if (existing != null)
                return new Response<AllowanceDto>(default!, "Allowance with the same name already exists.", true);

            var allowance = _mapper.Map<Allowance>(request);
            allowance.CreatedAt = DateTime.UtcNow;

            await _allowanceRepository.AddAsync(allowance);

            var createdDto = _mapper.Map<AllowanceDto>(allowance);
            return new Response<AllowanceDto>(createdDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<AllowanceDto>(default!, $"Error occurred while creating the allowance: {ex.Message}", true);
        }
    }

    public async Task<Response<AllowanceDto>> UpdateAsync(int id, UpdateAllowanceDto request)
    {
        try
        {
            var allowance = await _allowanceRepository.GetByIdAsync(id);
            if (allowance == null)
                return new Response<AllowanceDto>(default!, "Allowance not found.", true);

            if (!string.Equals(allowance.Name, request.Name, StringComparison.OrdinalIgnoreCase))
            {
                var existing = await _allowanceRepository.GetByNameAsync(request.Name);
                if (existing != null && existing.Id != id)
                    return new Response<AllowanceDto>(default!, "Another allowance with this name already exists.", true);
            }

            allowance.Name = request.Name;
            allowance.Amount = request.Amount;
            allowance.UpdatedAt = DateTime.UtcNow;

            await _allowanceRepository.UpdateAsync(allowance);

            var updatedDto = _mapper.Map<AllowanceDto>(allowance);
            return new Response<AllowanceDto>(updatedDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<AllowanceDto>(default!, $"Error occurred while updating the allowance: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            var allowance = await _allowanceRepository.GetByIdAsync(id);
            if (allowance == null)
                return new Response<bool>(false, "Allowance not found.", true);

            await _allowanceRepository.DeleteAsync(id);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while deleting the allowance: {ex.Message}", true);
        }
    }
}
