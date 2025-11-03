namespace HRManagementSystem.Application.Interfaces;

/// <summary>
/// Sample service interface - Replace with your actual service interfaces
/// </summary>
public interface ISampleService
{
    Task<Response<IEnumerable<string>>> GetAllAsync();
    Task<Response<string>> GetByIdAsync(Guid id);
}
