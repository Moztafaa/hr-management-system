using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Application.DTOs.Performance;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Application.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public PerformanceService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        // ===== Cycles =====
        public async Task<Response<ReviewCycleDto>> CreateCycleAsync(CreateCycleDto dto)
        {
            try
            {
                ReviewCycle entity = _mapper.Map<ReviewCycle>(dto);
                await _uow.ReviewCycles.AddAsync(entity);
                await _uow.SaveChangesAsync();
                return new Response<ReviewCycleDto>(_mapper.Map<ReviewCycleDto>(entity), "", false);
            }
            catch (Exception ex) { return new Response<ReviewCycleDto>(default!, ex.Message, true); }
        }

        public async Task<Response<IEnumerable<ReviewCycleDto>>> GetCyclesAsync()
        {
            try
            {
                IEnumerable<ReviewCycle> list = await _uow.ReviewCycles.GetAllAsync();
                return new Response<IEnumerable<ReviewCycleDto>>(
                    _mapper.Map<IEnumerable<ReviewCycleDto>>(list), "", false);
            }
            catch (Exception ex) { return new Response<IEnumerable<ReviewCycleDto>>(Array.Empty<ReviewCycleDto>(), ex.Message, true); }
        }

        // ===== Reviews =====
        public async Task<Response<PerformanceReviewDto>> CreateReviewAsync(CreateReviewDto dto)
        {
            try
            {
                ReviewCycle? reviewCycle = await _uow.ReviewCycles.GetByIdAsync(dto.ReviewCycleId);
                if (reviewCycle == null)
                    return new Response<PerformanceReviewDto>(default!, "Review Cycle not found", true);

                PerformanceReview entity = _mapper.Map<PerformanceReview>(dto);
                entity.Status = ReviewStatus.InProgress;
                await _uow.PerformanceReviews.AddAsync(entity);
                await _uow.SaveChangesAsync();
                return new Response<PerformanceReviewDto>(_mapper.Map<PerformanceReviewDto>(entity), "", false);
            }
            catch (Exception ex) { return new Response<PerformanceReviewDto>(default!, ex.Message, true); }
        }

        public async Task<Response<PerformanceReviewDto>> CloseReviewAsync(int reviewId, decimal? finalRating)
        {
            try
            {
                PerformanceReview? review = await _uow.PerformanceReviews.GetByIdAsync(reviewId);
                if (review is null)
                    return new Response<PerformanceReviewDto>(default!, "Review not found", true);

                review.Status = ReviewStatus.Closed;
                review.FinalRating = finalRating;
                await _uow.PerformanceReviews.UpdateAsync(review);
                await _uow.SaveChangesAsync();

                return new Response<PerformanceReviewDto>(_mapper.Map<PerformanceReviewDto>(review), "", false);
            }
            catch (Exception ex) { return new Response<PerformanceReviewDto>(default!, ex.Message, true); }
        }

        // ===== Goals =====
        public async Task<Response<GoalDto>> CreateGoalAsync(CreateGoalDto dto)
        {
            try
            {
                PerformanceReview? review = await _uow.PerformanceReviews.GetByIdAsync(dto.PerformanceReviewId);
                if (review is null)
                    return new Response<GoalDto>(default!, "Review not found", true);

                Goal entity = _mapper.Map<Goal>(dto);
                entity.Status = GoalStatus.NotStarted;
                await _uow.Goals.AddAsync(entity);
                await _uow.SaveChangesAsync();
                return new Response<GoalDto>(_mapper.Map<GoalDto>(entity), "", false);
            }
            catch (Exception ex) { return new Response<GoalDto>(default!, ex.Message, true); }
        }

        public async Task<Response<bool>> UpdateGoalProgressAsync(UpdateGoalProgressDto dto)
        {
            try
            {
                Goal? goal = await _uow.Goals.GetByIdAsync(dto.GoalId);
                if (goal is null)
                    return new Response<bool>(false, "Goal not found", true);

                goal.ProgressPercent = dto.ProgressPercent;
                if (!string.IsNullOrWhiteSpace(dto.Status))
                    goal.Status = Enum.Parse<GoalStatus>(dto.Status, true);

                await _uow.Goals.UpdateAsync(goal);
                await _uow.SaveChangesAsync();
                return new Response<bool>(true, "", false);
            }
            catch (Exception ex) { return new Response<bool>(false, ex.Message, true); }
        }

        // ===== KPIs =====
        public async Task<Response<KPIDto>> CreateKpiAsync(CreateKpiDto dto)
        {
            try
            {
                KPI entity = _mapper.Map<KPI>(dto);
                await _uow.KPIs.AddAsync(entity);
                await _uow.SaveChangesAsync();
                return new Response<KPIDto>(_mapper.Map<KPIDto>(entity), "", false);
            }
            catch (Exception ex) { return new Response<KPIDto>(default!, ex.Message, true); }
        }

        public async Task<Response<KPIResultDto>> AddKpiResultAsync(CreateKpiResultDto dto)
        {
            try
            {
                PerformanceReview? review = await _uow.PerformanceReviews.GetByIdAsync(dto.PerformanceReviewId);
                if (review is null)
                    return new Response<KPIResultDto>(default!, "Review not found", true);

                KPI? kpi = await _uow.KPIs.GetByIdAsync(dto.KpiId);
                if (kpi is null)
                    return new Response<KPIResultDto>(default!, "KPI not found", true);

                KPIResult result = new KPIResult { PerformanceReviewId = dto.PerformanceReviewId, KPIId = dto.KpiId, Actual = dto.Actual };
                await _uow.KPIResults.AddAsync(result);
                await _uow.SaveChangesAsync();

                return new Response<KPIResultDto>(_mapper.Map<KPIResultDto>(result), "", false);
            }
            catch (Exception ex) { return new Response<KPIResultDto>(default!, ex.Message, true); }
        }

        // ===== Feedback =====
        public async Task<Response<FeedbackDto>> AddFeedbackAsync(CreateFeedbackDto dto)
        {
            try
            {
                PerformanceReview? review = await _uow.PerformanceReviews.GetByIdAsync(dto.PerformanceReviewId);
                if (review is null)
                    return new Response<FeedbackDto>(default!, "Review not found", true);

                Feedback entity = _mapper.Map<Feedback>(dto);
                await _uow.Feedbacks.AddAsync(entity);
                await _uow.SaveChangesAsync();

                return new Response<FeedbackDto>(_mapper.Map<FeedbackDto>(entity), "", false);
            }
            catch (Exception ex) { return new Response<FeedbackDto>(default!, ex.Message, true); }
        }

        // ===== Competencies =====
        public async Task<Response<CompetencyDto>> CreateCompetencyAsync(CreateCompetencyDto dto)
        {
            try
            {
                Competency entity = _mapper.Map<Competency>(dto);
                await _uow.Competencies.AddAsync(entity);
                await _uow.SaveChangesAsync();
                return new Response<CompetencyDto>(_mapper.Map<CompetencyDto>(entity), "", false);
            }
            catch (Exception ex) { return new Response<CompetencyDto>(default!, ex.Message, true); }
        }

        public async Task<Response<EmployeeCompetencyRatingDto>> RateCompetencyAsync(RateCompetencyDto dto)
        {
            try
            {
                PerformanceReview? review = await _uow.PerformanceReviews.GetByIdAsync(dto.PerformanceReviewId);
                if (review is null)
                    return new Response<EmployeeCompetencyRatingDto>(default!, "Review not found", true);

                Competency? comp = await _uow.Competencies.GetByIdAsync(dto.CompetencyId);
                if (comp is null)
                    return new Response<EmployeeCompetencyRatingDto>(default!, "Competency not found", true);

                EmployeeCompetencyRating rating = new EmployeeCompetencyRating
                {
                    PerformanceReviewId = dto.PerformanceReviewId,
                    CompetencyId = dto.CompetencyId,
                    Rating = dto.Rating,
                    Notes = dto.Notes
                };

                await _uow.EmployeeCompetencyRatings.AddAsync(rating);
                await _uow.SaveChangesAsync();

                return new Response<EmployeeCompetencyRatingDto>(_mapper.Map<EmployeeCompetencyRatingDto>(rating), "", false);
            }
            catch (Exception ex) { return new Response<EmployeeCompetencyRatingDto>(default!, ex.Message, true); }
        }

        // ===== Report =====
        public async Task<Response<PerformanceReportDto>> GetReviewReportAsync(int reviewId)
        {
            try
            {
                PerformanceReview? review = await _uow.PerformanceReviews.GetByIdAsync(reviewId);
                if (review is null)
                    return new Response<PerformanceReportDto>(default!, "Review not found", true);

                IEnumerable<Goal> goals = await _uow.Goals.GetAllAsync();
                IEnumerable<KPIResult> kpiRes = await _uow.KPIResults.GetAllAsync();
                IEnumerable<EmployeeCompetencyRating> comps = await _uow.EmployeeCompetencyRatings.GetAllAsync();
                IEnumerable<Feedback> feeds = await _uow.Feedbacks.GetAllAsync();

                PerformanceReportDto dto = new PerformanceReportDto
                {
                    ReviewId = review.Id,
                    FinalRating = review.FinalRating,
                    Goals = _mapper.Map<List<GoalDto>>(goals),
                    KpiResults = _mapper.Map<List<KPIResultDto>>(kpiRes),
                    CompetencyRatings = _mapper.Map<List<EmployeeCompetencyRatingDto>>(comps),
                    Feedbacks = _mapper.Map<List<FeedbackDto>>(feeds)
                };

                return new Response<PerformanceReportDto>(dto, "", false);
            }
            catch (Exception ex)
            {
                return new Response<PerformanceReportDto>(default!, ex.Message, true);
            }
        }
    }
}
