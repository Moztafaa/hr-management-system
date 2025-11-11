using HRManagementSystem.Application.DTOs.JobPosting;
using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Application.Mappings;

public class JobPostingProfile : Profile
{
    public JobPostingProfile()
    {
        CreateMap<JobPosting, JobPostingDto>()
            .ForMember(dest => dest.DepartmentName,
                opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
            .ForMember(dest => dest.DesignationName,
                opt => opt.MapFrom(src => src.Designation != null ? src.Designation.Title : null));

        CreateMap<CreateJobPostingDto, JobPosting>();
        CreateMap<UpdateJobPostingDto, JobPosting>();

        CreateMap<JobPosting, JobPostingDetailDto>()
            .ForMember(dest => dest.DepartmentName,
                opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : null))
            .ForMember(dest => dest.DesignationName,
                opt => opt.MapFrom(src => src.Designation != null ? src.Designation.Title : null));
    }
}
