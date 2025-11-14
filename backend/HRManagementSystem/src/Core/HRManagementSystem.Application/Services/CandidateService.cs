using AutoMapper;
using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.Candidate;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Interfaces;

namespace HRManagementSystem.Application.Services;

public class CandidateService(ICandidateRepository candidateRepository, IMapper mapper) : ICandidateService
{
    public async Task<Response<CandidateDto>> GetCandidateByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            Candidate? candidate = await candidateRepository.GetByIdAsync(id, cancellationToken);
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
            IEnumerable<Candidate> candidates = await candidateRepository.GetAllAsync(cancellationToken);
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
            Candidate? candidate = await candidateRepository.GetByEmailAsync(email, cancellationToken);
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
            Candidate? candidate = await candidateRepository.GetCandidatewithApplicationsAsync(id, cancellationToken);
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
            IEnumerable<Candidate> candidates = await candidateRepository.SearchCandidatesAsync(searchTerm, cancellationToken);
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
            bool emailExists = await candidateRepository.IsEmailInUseAsync(createCandidateDto.Email, cancellationToken);
            if (emailExists)
            {
                return new Response<CandidateDto>(null!, "A candidate with this email already exists.", true);
            }

            Candidate candidate = mapper.Map<Candidate>(createCandidateDto);
            candidate.CreatedAt = DateTime.UtcNow;

            await candidateRepository.AddAsync(candidate, cancellationToken);

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
            Candidate? candidate = await candidateRepository.GetByIdAsync(id, cancellationToken);
            if (candidate == null)
            {
                return new Response<bool>(false, "Candidate not found.", true);
            }

            // Check if email is being changed and if the new email already exists
            if (updateCandidateDto.Email != null && updateCandidateDto.Email != candidate.Email)
            {
                bool emailExists = await candidateRepository.IsEmailInUseAsync(updateCandidateDto.Email, cancellationToken);
                if (emailExists)
                {
                    return new Response<bool>(false, "A candidate with this email already exists.", true);
                }
            }

            mapper.Map(updateCandidateDto, candidate);
            candidate.UpdatedAt = DateTime.UtcNow;

            await candidateRepository.UpdateAsync(candidate, cancellationToken);
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
            Candidate? candidate = await candidateRepository.GetByIdAsync(id, cancellationToken);
            if (candidate == null)
            {
                return new Response<bool>(false, "Candidate not found.", true);
            }

            await candidateRepository.DeleteAsync(id, cancellationToken);
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
            bool exists = await candidateRepository.IsEmailInUseAsync(email, cancellationToken);
            return new Response<bool>(exists, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error occurred while checking email: {ex.Message}", true);
        }
    }
}
