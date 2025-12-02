using HRManagementSystem.Application.Common;

namespace HRManagementSystem.Application.Interfaces;

public interface IAIChatService
{
    Task<Response<string>> GetChatCompletionAsync(string prompt, CancellationToken cancellationToken = default);
}
