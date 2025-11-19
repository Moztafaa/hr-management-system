namespace HRManagementSystem.Application.Services;

/// <summary>
/// Sample service implementation - Replace with your actual services
/// </summary>
public class SampleService : ISampleService
{
    public async Task<Response<IEnumerable<string>>> GetAllAsync()
    {
        try
        {
            await Task.CompletedTask;
            var data = new List<string> { "Sample1", "Sample2" };
            return new Response<IEnumerable<string>>(data, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<string>>(null!, ex.Message, true);
        }
    }

    public async Task<Response<string>> GetByIdAsync(Guid id)
    {
        try
        {
            await Task.CompletedTask;
            string data = $"Sample-{id}";
            return new Response<string>(data, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<string>(null!, ex.Message, true);
        }
    }
}

