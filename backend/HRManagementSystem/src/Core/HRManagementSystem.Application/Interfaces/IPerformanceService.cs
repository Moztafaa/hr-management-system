using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.Performance;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Application.Interfaces
{
    public interface IPerformanceService
    {
        // Cycles
        Task<Response<ReviewCycleDto>> CreateCycleAsync(CreateCycleDto dto);
        Task<Response<IEnumerable<ReviewCycleDto>>> GetCyclesAsync();
        Task<Response<IEnumerable<ReviewCycleDto>>> GetActiveCyclesAsync(DateTime? at = null);

        // Reviews
        Task<Response<PerformanceReviewDto>> CreateReviewAsync(CreateReviewDto dto);
        Task<Response<PerformanceReviewDto>> CloseReviewAsync(int reviewId, decimal? finalRating);
        Task<Response<IEnumerable<PerformanceReviewDto>>> GetEmployeeReviewsAsync(int employeeId);

        // Goals
        Task<Response<GoalDto>> CreateGoalAsync(CreateGoalDto dto);
        Task<Response<bool>> UpdateGoalProgressAsync(UpdateGoalProgressDto dto);

        // KPIs
        Task<Response<KPIDto>> CreateKpiAsync(CreateKpiDto dto);
        Task<Response<KPIResultDto>> AddKpiResultAsync(CreateKpiResultDto dto);

        // Feedback
        Task<Response<FeedbackDto>> AddFeedbackAsync(CreateFeedbackDto dto);
        Task<Response<IEnumerable<FeedbackDto>>> GetFeedbackByTypeAsync(int reviewId, FeedbackType type);

        // Competencies
        Task<Response<CompetencyDto>> CreateCompetencyAsync(CreateCompetencyDto dto);
        Task<Response<EmployeeCompetencyRatingDto>> RateCompetencyAsync(RateCompetencyDto dto);

        // Reports
        Task<Response<PerformanceReportDto>> GetReviewReportAsync(int reviewId);
    }
}
