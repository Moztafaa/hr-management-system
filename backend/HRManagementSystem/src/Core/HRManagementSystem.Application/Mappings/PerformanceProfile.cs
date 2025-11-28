using HRManagementSystem.Application.DTOs.Performance;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Application.Mappings;
public class PerformanceProfile : Profile
{
        public PerformanceProfile()
        {
            CreateMap<CreateCycleDto, ReviewCycle>();
            CreateMap<ReviewCycle, ReviewCycleDto>();

            CreateMap<CreateReviewDto, PerformanceReview>();
            CreateMap<PerformanceReview, PerformanceReviewDto>();

            CreateMap<CreateGoalDto, Goal>();
            CreateMap<Goal, GoalDto>();

            CreateMap<CreateKpiDto, KPI>();
            CreateMap<KPI, KPIDto>();

            CreateMap<KPIResult, KPIResultDto>();

            CreateMap<CreateFeedbackDto, Feedback>();
            CreateMap<Feedback, FeedbackDto>();

            CreateMap<CreateCompetencyDto, Competency>();
            CreateMap<Competency, CompetencyDto>();

            CreateMap<EmployeeCompetencyRating, EmployeeCompetencyRatingDto>();
        }
    }



