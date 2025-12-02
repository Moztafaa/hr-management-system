using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.Chat;

namespace HRManagementSystem.Application.Interfaces;

public interface IRAGService
{
    Task<Response<ChatResponseDto>> AskQuestionAsync(string question, CancellationToken cancellationToken = default);
    Task<Response<bool>> AddContextAsync(string entityType, string text, CancellationToken cancellationToken = default);
    Task<Response<int>> SyncAllEmployeesAsync(CancellationToken cancellationToken = default);
}
