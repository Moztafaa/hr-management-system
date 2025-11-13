namespace HRManagementSystem.Application.Interfaces;

public interface IAllowanceService
{
    Task<Response<IEnumerable<AllowanceDto>>> GetAllAsync();
    Task<Response<AllowanceDto>> GetByIdAsync(int id);
    Task<Response<AllowanceDto>> CreateAsync(CreateAllowanceDto request);
    Task<Response<AllowanceDto>> UpdateAsync(int id, UpdateAllowanceDto request);
    Task<Response<bool>> DeleteAsync(int id);
}
