using AutoMapper;
using HRManagementSystem.Application.DTOs.Candidate;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Application.Mappings;

public class CandidateProfile : Profile
{
    public CandidateProfile()
    {
        CreateMap<Candidate, CandidateDto>().ReverseMap();
        CreateMap<CreateCandidateDto, Candidate>();
        CreateMap<UpdateCandidateDto, Candidate>();

        CreateMap<Candidate, CandidateSummaryDto>()
            .ForMember(dest => dest.FullName,
                opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.TotalApplications,
                opt => opt.MapFrom(src => src.JobApplications.Count));
    }
}
