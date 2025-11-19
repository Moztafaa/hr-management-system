using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.Interfaces;
// Application/Interfaces/IPerformanceService.cs
using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.Performance;

public interface IPerformanceService
{
    // Cycles
    Task<Response<ReviewCycleDto>> CreateCycleAsync(CreateCycleDto dto);
    Task<Response<IEnumerable<ReviewCycleDto>>> GetCyclesAsync();

    // Reviews
    Task<Response<PerformanceReviewDto>> CreateReviewAsync(CreateReviewDto dto);
    Task<Response<PerformanceReviewDto>> CloseReviewAsync(int reviewId, decimal? finalRating);

    // Goals
    Task<Response<GoalDto>> CreateGoalAsync(CreateGoalDto dto);
    Task<Response<bool>> UpdateGoalProgressAsync(UpdateGoalProgressDto dto);

    // KPIs
    Task<Response<KPIDto>> CreateKpiAsync(CreateKpiDto dto);
    Task<Response<KPIResultDto>> AddKpiResultAsync(CreateKpiResultDto dto);

    // Feedback (Self/Manager/Peer = 360)
    Task<Response<FeedbackDto>> AddFeedbackAsync(CreateFeedbackDto dto);

    // Competencies
    Task<Response<CompetencyDto>> CreateCompetencyAsync(CreateCompetencyDto dto);
    Task<Response<EmployeeCompetencyRatingDto>> RateCompetencyAsync(RateCompetencyDto dto);

    // Reports
    Task<Response<PerformanceReportDto>> GetReviewReportAsync(int reviewId);
}
