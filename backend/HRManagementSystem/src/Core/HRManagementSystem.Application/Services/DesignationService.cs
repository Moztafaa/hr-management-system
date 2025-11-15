namespace HRManagementSystem.Application.Services;

public class DesignationService(IUnitOfWork _unitOfWork, IMapper _mapper) : IDesignationService
{
    public async Task<Response<DesignationDto>> CreateDesignationAsync(CreateDesignationDto createDesignationDto, CancellationToken cancellationToken = default)
    {
        try
        {
            Designation existingDesignation = await _unitOfWork.Designations.GetByTitleAsync(createDesignationDto.Title, cancellationToken);
            if (existingDesignation != null)
            {
                return new Response<DesignationDto>(default!, "Designation with the same name already exists.", true);
            }
            Designation? Designation = _mapper.Map<Designation>(createDesignationDto);
            await _unitOfWork.Designations.AddAsync(Designation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            DesignationDto? DesignationDto = _mapper.Map<DesignationDto>(Designation);

            return new Response<DesignationDto>(DesignationDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            // Log the exception (ex) as needed
            return new Response<DesignationDto>(default!, $"Error occurred while creating the Designation: {ex.Message}", true);
        }
    }


    public async Task<Response<bool>> DeleteDesignationAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            Designation? Designation = await _unitOfWork.Designations.GetByIdAsync(id, cancellationToken);
            if (Designation == null)
            {
                return new Response<bool>(false, "Designation not found.", true);
            }

            // Check if Designation has employees
            if (Designation.Employees != null && Designation.Employees.Any())
            {
                return new Response<bool>(false, "Cannot delete Designation with existing employees. Please reassign or remove employees first.", true);
            }

            await _unitOfWork.Designations.DeleteAsync(Designation.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            // Log the exception (ex) as needed
            return new Response<bool>(false, $"Error occurred while deleting the Designation: {ex.Message}", true);
        }

    }

    public async Task<Response<IEnumerable<DesignationDto>>> GetAllDesignationsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Designation> Designations = await _unitOfWork.Designations.GetAllAsync(cancellationToken);
            IEnumerable<DesignationDto> DesignationDtos = Designations.Select(
                d => _mapper.Map<DesignationDto>(d));
            return new Response<IEnumerable<DesignationDto>>(DesignationDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<DesignationDto>>(null!, $"Error occurred while retrieving Designations: {ex.Message}", true);

        }
    }
    public async Task<Response<DesignationDto>> GetDesignationByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            Designation? Designation = await _unitOfWork.Designations.GetByIdAsync(id, cancellationToken);
            if (Designation == null)
            {
                return new Response<DesignationDto>(null!, "Designation not found.", true);
            }

            DesignationDto? DesignationDto = _mapper.Map<DesignationDto>(Designation);

            return new Response<DesignationDto>(DesignationDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<DesignationDto>(null!, $"Error occurred while retrieving the Designation: {ex.Message}", true);
        }
    }
    public async Task<Response<DesignationWithEmployeesDto>> GetDesignationWithEmployeesAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Designation> Designations = await _unitOfWork.Designations.GetDesignationsWithEmployeesAsync(id, cancellationToken);
            Designation? Designation = Designations.FirstOrDefault();
            if (Designation == null)
            {
                return new Response<DesignationWithEmployeesDto>(default!, "Designation not found.", true);
            }

            DesignationWithEmployeesDto? DesignationWithEmployeesDto = _mapper.Map<DesignationWithEmployeesDto>(Designation);

            return new Response<DesignationWithEmployeesDto>(DesignationWithEmployeesDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<DesignationWithEmployeesDto>(null!, $"Error occurred while retrieving the Designation with employees: {ex.Message}", true);
        }
    }
    public async Task<Response<bool>> UpdateDesignationAsync(int id, UpdateDesignationDto updateDesignationDto, CancellationToken cancellationToken = default)
    {
        try
        {
            Designation? Designation = await _unitOfWork.Designations.GetByIdAsync(id, cancellationToken);
            if (Designation == null)
            {
                return new Response<bool>(false, "Designation not found.", true);
            }

            // Check if title is being changed and if it conflicts with another Designation
            if (Designation.Title != updateDesignationDto.Title)
            {
                Designation? existingDept = await _unitOfWork.Designations.GetByTitleAsync(updateDesignationDto.Title, cancellationToken);
                if (existingDept != null && existingDept.Id != id)
                {
                    return new Response<bool>(false, "Another Designation with this name already exists.", true);
                }
            }

            Designation.Title = updateDesignationDto.Title; // Changed from Name to Title
            Designation.Description = updateDesignationDto.Description;
            Designation.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Designations.UpdateAsync(Designation, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            // Log the exception (ex) as needed
            return new Response<bool>(false, $"Error occurred while updating the Designation: {ex.Message}", true);
        }
    }
}
