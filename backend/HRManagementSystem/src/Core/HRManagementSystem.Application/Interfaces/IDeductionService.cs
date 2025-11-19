namespace HRManagementSystem.Application.Interfaces;

public interface IDeductionService
{
    Task<Response<IEnumerable<DeductionDto>>> GetAllAsync();
    Task<Response<DeductionDto>> GetByIdAsync(int id);
    Task<Response<DeductionDto>> CreateAsync(CreateDeductionDto request);
    Task<Response<DeductionDto>> UpdateAsync(int id, UpdateDeductionDto request);
    Task<Response<bool>> DeleteAsync(int id);
}
