namespace HRManagementSystem.Application.Services;


public class AllowanceService(IUnitOfWork _unitOfWork, IMapper _mapper) : IAllowanceService

{


    public async Task<Response<IEnumerable<AllowanceDto>>> GetAllAsync()
    {
        try
        {

            IEnumerable<Allowance> allowances = await _unitOfWork.Allowances.GetAllAsync();
            IEnumerable<AllowanceDto> allowanceDtos = _mapper.Map<IEnumerable<AllowanceDto>>(allowances);


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
            Allowance? allowance = await _unitOfWork.Allowances.GetByIdAsync(id);
            if (allowance == null)
                return new Response<AllowanceDto>(null!, "Allowance not found.", true);

            AllowanceDto allowanceDto = _mapper.Map<AllowanceDto>(allowance);
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
            Allowance? existing = await _unitOfWork.Allowances.GetByNameAsync(request.Name);
            if (existing != null)
                return new Response<AllowanceDto>(default!, "Allowance with the same name already exists.", true);

            Allowance allowance = _mapper.Map<Allowance>(request);
            allowance.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.Allowances.AddAsync(allowance);
            await _unitOfWork.SaveChangesAsync();

            AllowanceDto createdDto = _mapper.Map<AllowanceDto>(allowance);
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
            Allowance? allowance = await _unitOfWork.Allowances.GetByIdAsync(id);
            if (allowance == null)
                return new Response<AllowanceDto>(default!, "Allowance not found.", true);

            // Only check for name conflict if Name is provided
            if (!string.IsNullOrEmpty(request.Name) && !string.Equals(allowance.Name, request.Name, StringComparison.OrdinalIgnoreCase))
            {
                Allowance? existing = await _unitOfWork.Allowances.GetByNameAsync(request.Name);
                if (existing != null && existing.Id != id)
                    return new Response<AllowanceDto>(default!, "Another allowance with this name already exists.", true);
            }

            // Apply updates only if provided
            if (!string.IsNullOrEmpty(request.Name))
                allowance.Name = request.Name;
            if (request.Amount.HasValue)
                allowance.Amount = request.Amount.Value;
            if (request.IsPercentage.HasValue)
                allowance.IsPercentage = request.IsPercentage.Value;
            allowance.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Allowances.UpdateAsync(allowance);
            await _unitOfWork.SaveChangesAsync();

            AllowanceDto updatedDto = _mapper.Map<AllowanceDto>(allowance);
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
            Allowance? allowance = await _unitOfWork.Allowances.GetByIdAsync(id);
            if (allowance == null)
                return new Response<bool>(false, "Allowance not found.", true);

            await _unitOfWork.Allowances.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while deleting the allowance: {ex.Message}", true);
        }
    }
}
