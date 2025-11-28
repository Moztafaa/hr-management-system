using AutoMapper;
using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.Candidate;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Interfaces;

namespace HRManagementSystem.Application.Services;

public class CandidateService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService, INotificationService notificationService) : ICandidateService
{
    public async Task<Response<CandidateDto>> GetCandidateByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            Candidate? candidate = await unitOfWork.Candidates.GetByIdAsync(id, cancellationToken);
            if (candidate == null)
            {
                return new Response<CandidateDto>(null!, "Candidate not found.", true);
            }

            CandidateDto candidateDto = mapper.Map<CandidateDto>(candidate);
            return new Response<CandidateDto>(candidateDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<CandidateDto>(null!, $"Error occurred while retrieving the candidate: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<CandidateDto>>> GetAllCandidatesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Candidate> candidates = await unitOfWork.Candidates.GetAllAsync(cancellationToken);
            IEnumerable<CandidateDto> candidateDtos = mapper.Map<IEnumerable<CandidateDto>>(candidates);

            return new Response<IEnumerable<CandidateDto>>(candidateDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<CandidateDto>>(null!, $"Error occurred while retrieving candidates: {ex.Message}", true);
        }
    }

    public async Task<Response<CandidateDto>> GetCandidateByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            Candidate? candidate = await unitOfWork.Candidates.GetByEmailAsync(email, cancellationToken);
            if (candidate == null)
            {
                return new Response<CandidateDto>(null!, "Candidate not found with the provided email.", true);
            }

            CandidateDto candidateDto = mapper.Map<CandidateDto>(candidate);
            return new Response<CandidateDto>(candidateDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<CandidateDto>(null!, $"Error occurred while retrieving the candidate: {ex.Message}", true);
        }
    }

    public async Task<Response<CandidateDto>> GetCandidateWithApplicationsAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            Candidate? candidate = await unitOfWork.Candidates.GetCandidatewithApplicationsAsync(id, cancellationToken);
            if (candidate == null)
            {
                return new Response<CandidateDto>(null!, "Candidate not found.", true);
            }

            CandidateDto candidateDto = mapper.Map<CandidateDto>(candidate);
            return new Response<CandidateDto>(candidateDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<CandidateDto>(null!, $"Error occurred while retrieving the candidate with applications: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<CandidateDto>>> SearchCandidatesAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        try
        {
            IEnumerable<Candidate> candidates = await unitOfWork.Candidates.SearchCandidatesAsync(searchTerm, cancellationToken);
            IEnumerable<CandidateDto> candidateDtos = mapper.Map<IEnumerable<CandidateDto>>(candidates);

            return new Response<IEnumerable<CandidateDto>>(candidateDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<CandidateDto>>(null!, $"Error occurred while searching candidates: {ex.Message}", true);
        }
    }

    public async Task<Response<CandidateDto>> CreateCandidateAsync(CreateCandidateDto createCandidateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check if email already exists
            bool emailExists = await unitOfWork.Candidates.IsEmailInUseAsync(createCandidateDto.Email, cancellationToken);
            if (emailExists)
            {
                return new Response<CandidateDto>(null!, "A candidate with this email already exists.", true);
            }

            Candidate candidate = mapper.Map<Candidate>(createCandidateDto);
            candidate.CreatedAt = DateTime.UtcNow;

            await unitOfWork.Candidates.AddAsync(candidate, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            // Send welcome email to candidate
            try
            {
                if (!string.IsNullOrEmpty(candidate.Email))
                {
                    await emailService.SendWelcomeEmailAsync(
                        candidate.Email,
                        $"{candidate.FirstName} {candidate.LastName}",
                        cancellationToken);
                }
            }
            catch (Exception emailEx)
            {
                Console.WriteLine($"Failed to send welcome email to candidate: {emailEx.Message}");
            }

            CandidateDto candidateDto = mapper.Map<CandidateDto>(candidate);
            return new Response<CandidateDto>(candidateDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<CandidateDto>(null!, $"Error occurred while creating the candidate: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> UpdateCandidateAsync(int id, UpdateCandidateDto updateCandidateDto, CancellationToken cancellationToken = default)
    {
        try
        {
            Candidate? candidate = await unitOfWork.Candidates.GetByIdAsync(id, cancellationToken);
            if (candidate == null)
            {
                return new Response<bool>(false, "Candidate not found.", true);
            }

            // Check if email is being changed and if the new email already exists
            if (updateCandidateDto.Email != null && updateCandidateDto.Email != candidate.Email)
            {
                bool emailExists = await unitOfWork.Candidates.IsEmailInUseAsync(updateCandidateDto.Email, cancellationToken);
                if (emailExists)
                {
                    return new Response<bool>(false, "A candidate with this email already exists.", true);
                }
            }

            mapper.Map(updateCandidateDto, candidate);
            candidate.UpdatedAt = DateTime.UtcNow;

            await unitOfWork.Candidates.UpdateAsync(candidate, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while updating the candidate: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> DeleteCandidateAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            Candidate? candidate = await unitOfWork.Candidates.GetByIdAsync(id, cancellationToken);
            if (candidate == null)
            {
                return new Response<bool>(false, "Candidate not found.", true);
            }

            await unitOfWork.Candidates.DeleteAsync(id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while deleting the candidate: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> CheckEmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        try
        {
            bool exists = await unitOfWork.Candidates.IsEmailInUseAsync(email, cancellationToken);
            return new Response<bool>(exists, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while checking email: {ex.Message}", true);
        }
    }
}
