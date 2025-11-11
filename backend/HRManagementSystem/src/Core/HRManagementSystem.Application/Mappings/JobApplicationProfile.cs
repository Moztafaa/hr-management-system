using HRManagementSystem.Application.DTOs.JobApplication;

namespace HRManagementSystem.Application.Mappings;

public class JobApplicationProfile : Profile
{
    public JobApplicationProfile()
    {
        // Entity to DTO - convert enums to strings
        CreateMap<JobApplication, JobApplicationDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source.ToString()))
            .ForMember(dest => dest.CurrentStage, opt => opt.MapFrom(src => src.CurrentStage.ToString()));

        CreateMap<JobApplication, JobApplicationDetailDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source.ToString()))
            .ForMember(dest => dest.CurrentStage, opt => opt.MapFrom(src => src.CurrentStage.ToString()));

        // DTO to Entity - convert strings to enums
        CreateMap<CreateJobApplicationDto, JobApplication>()
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => Enum.Parse<ApplicationSource>(src.Source, true)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ApplicationStatus.Applied))
            .ForMember(dest => dest.CurrentStage, opt => opt.MapFrom(src => RecruitmentStage.Submitted));

        CreateMap<UpdateJobApplicationStatusDto, JobApplication>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.JobApplicationId))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.Parse<ApplicationStatus>(src.Status, true)))
            .ForMember(dest => dest.CurrentStage, opt => opt.MapFrom(src =>
                !string.IsNullOrEmpty(src.CurrentStage)
                    ? Enum.Parse<RecruitmentStage>(src.CurrentStage, true)
                    : (RecruitmentStage?)null))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}

