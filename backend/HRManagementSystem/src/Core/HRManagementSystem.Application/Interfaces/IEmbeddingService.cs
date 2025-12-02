using HRManagementSystem.Application.Common;

namespace HRManagementSystem.Application.Interfaces;

public interface IEmbeddingService
{
    Task<Response<List<float>>> ConvertToEmbeddingAsync(string text, CancellationToken cancellationToken = default);
}
