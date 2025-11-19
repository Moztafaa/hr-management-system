using HRManagementSystem.Application.DTOs.Performance;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Application.Mappings;
public class PerformanceProfile : Profile
{
    public PerformanceProfile()
    {
        CreateMap<CreateCycleDto, ReviewCycle>()
        .ForMember(d => d.Frequency, m => m.MapFrom(s => MapCycleFrequency(s.Frequency)))
        .ForMember(d => d.RatingScale, m => m.MapFrom(s => MapRatingScale(s.RatingScale)));

        CreateMap<ReviewCycle, ReviewCycleDto>()
            .ForMember(d => d.Frequency, m => m.MapFrom(s => s.Frequency.ToString()))
            .ForMember(d => d.RatingScale, m => m.MapFrom(s => s.RatingScale.ToString()));

        CreateMap<CreateReviewDto, PerformanceReview>()
            .ForMember(d => d.Status, m => m.MapFrom(_ => ReviewStatus.InProgress));

        CreateMap<PerformanceReview, PerformanceReviewDto>()
            .ForMember(d => d.Status, m => m.MapFrom(s => s.Status.ToString()));

        CreateMap<CreateGoalDto, Goal>();
        CreateMap<Goal, GoalDto>()
            .ForMember(d => d.Status, m => m.MapFrom(s => s.Status.ToString()));

        CreateMap<CreateKpiDto, KPI>();
        CreateMap<KPI, KPIDto>();

        CreateMap<KPIResult, KPIResultDto>();

        CreateMap<CreateFeedbackDto, Feedback>()
            .ForMember(d => d.Type, m => m.MapFrom(s => Enum.Parse<FeedbackType>(s.Type, true)));

        CreateMap<Feedback, FeedbackDto>()
            .ForMember(d => d.Type, m => m.MapFrom(s => s.Type.ToString()));

        CreateMap<CreateCompetencyDto, Competency>();
        CreateMap<Competency, CompetencyDto>();

        CreateMap<EmployeeCompetencyRating, EmployeeCompetencyRatingDto>();
    }

    private static CycleFrequency MapCycleFrequency(string frequency)
    {
        return Enum.TryParse<CycleFrequency>(frequency, true, out CycleFrequency result) ? result : CycleFrequency.Annual;
    }

    private static RatingScaleType MapRatingScale(string ratingScale)
    {
        return Enum.TryParse<RatingScaleType>(ratingScale, true, out RatingScaleType result) ? result : RatingScaleType.OneToFive; 
    }
}

