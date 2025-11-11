using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.Candidate;

namespace HRManagementSystem.Application.Interfaces;

public interface ICandidateService
{
    Task<Response<CandidateDto>> GetCandidateByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<CandidateDto>>> GetAllCandidatesAsync(CancellationToken cancellationToken = default);
    Task<Response<CandidateDto>> GetCandidateByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Response<CandidateDto>> GetCandidateWithApplicationsAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<CandidateDto>>> SearchCandidatesAsync(string searchTerm, CancellationToken cancellationToken = default);
    Task<Response<CandidateDto>> CreateCandidateAsync(CreateCandidateDto createCandidateDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> UpdateCandidateAsync(int id, UpdateCandidateDto updateCandidateDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteCandidateAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<bool>> CheckEmailExistsAsync(string email, CancellationToken cancellationToken = default);
}
