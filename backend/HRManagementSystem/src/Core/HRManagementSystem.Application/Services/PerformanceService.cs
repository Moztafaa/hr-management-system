using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.Performance;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;
using HRManagementSystem.Domain.Enums.Performance;
using HRManagementSystem.Domain.Interfaces;

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

        // ===================== Cycles =====================
        public async Task<Response<ReviewCycleDto>> CreateCycleAsync(CreateCycleDto dto)
        {
            try
            {
                ReviewCycle entity = _mapper.Map<ReviewCycle>(dto);
                await _uow.ReviewCycles.AddAsync(entity);
                await _uow.SaveChangesAsync();

                ReviewCycleDto result = _mapper.Map<ReviewCycleDto>(entity);
                return new Response<ReviewCycleDto>(result, "", false);
            }
            catch (Exception ex)
            {
                return new Response<ReviewCycleDto>(default!, ex.Message, true);
            }
        }

        public async Task<Response<IEnumerable<ReviewCycleDto>>> GetCyclesAsync()
        {
            try
            {
                IEnumerable<ReviewCycle> list = await _uow.ReviewCycles.GetAllAsync();
                IEnumerable<ReviewCycleDto> result = _mapper.Map<IEnumerable<ReviewCycleDto>>(list);

                return new Response<IEnumerable<ReviewCycleDto>>(result, "", false);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ReviewCycleDto>>(Array.Empty<ReviewCycleDto>(), ex.Message, true);
            }
        }

        public async Task<Response<IEnumerable<ReviewCycleDto>>> GetActiveCyclesAsync(DateTime? at = null)
        {
            try
            {
                DateTime checkDate = at ?? DateTime.UtcNow;
                IEnumerable<ReviewCycle> list = await _uow.ReviewCycles.GetActiveAtAsync(checkDate);
                IEnumerable<ReviewCycleDto> result = _mapper.Map<IEnumerable<ReviewCycleDto>>(list);

                return new Response<IEnumerable<ReviewCycleDto>>(result, "", false);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ReviewCycleDto>>(Array.Empty<ReviewCycleDto>(), ex.Message, true);
            }
        }

        // ===================== Reviews =====================
        public async Task<Response<PerformanceReviewDto>> CreateReviewAsync(CreateReviewDto dto)
        {
            try
            {
                ReviewCycle? reviewCycle = await _uow.ReviewCycles.GetByIdAsync(dto.ReviewCycleId);
                if (reviewCycle is null)
                    return new Response<PerformanceReviewDto>(default!, "Review Cycle not found", true);

                PerformanceReview entity = _mapper.Map<PerformanceReview>(dto);
                entity.Status = ReviewStatus.InProgress;

                await _uow.PerformanceReviews.AddAsync(entity);
                await _uow.SaveChangesAsync();

                PerformanceReviewDto result = _mapper.Map<PerformanceReviewDto>(entity);
                return new Response<PerformanceReviewDto>(result, "", false);
            }
            catch (Exception ex)
            {
                return new Response<PerformanceReviewDto>(default!, ex.Message, true);
            }
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

                PerformanceReviewDto result = _mapper.Map<PerformanceReviewDto>(review);
                return new Response<PerformanceReviewDto>(result, "", false);
            }
            catch (Exception ex)
            {
                return new Response<PerformanceReviewDto>(default!, ex.Message, true);
            }
        }

        public async Task<Response<IEnumerable<PerformanceReviewDto>>> GetEmployeeReviewsAsync(int employeeId)
        {
            try
            {
                IEnumerable<PerformanceReview> reviews = await _uow.PerformanceReviews.GetByEmployeeAsync(employeeId);
                IEnumerable<PerformanceReviewDto> result = _mapper.Map<IEnumerable<PerformanceReviewDto>>(reviews);

                return new Response<IEnumerable<PerformanceReviewDto>>(result, "", false);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<PerformanceReviewDto>>(Array.Empty<PerformanceReviewDto>(), ex.Message, true);
            }
        }

        // ===================== Goals =====================
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

                GoalDto result = _mapper.Map<GoalDto>(entity);
                return new Response<GoalDto>(result, "", false);
            }
            catch (Exception ex)
            {
                return new Response<GoalDto>(default!, ex.Message, true);
            }
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
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, true);
            }
        }

        // ===================== KPIs =====================
        public async Task<Response<KPIDto>> CreateKpiAsync(CreateKpiDto dto)
        {
            try
            {
                KPI entity = _mapper.Map<KPI>(dto);
                await _uow.KPIs.AddAsync(entity);
                await _uow.SaveChangesAsync();

                KPIDto result = _mapper.Map<KPIDto>(entity);
                return new Response<KPIDto>(result, "", false);
            }
            catch (Exception ex)
            {
                return new Response<KPIDto>(default!, ex.Message, true);
            }
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

                KPIResult resultEntity = new KPIResult
                {
                    PerformanceReviewId = dto.PerformanceReviewId,
                    KPIId = dto.KpiId,
                    Actual = dto.Actual
                };

                await _uow.KPIResults.AddAsync(resultEntity);
                await _uow.SaveChangesAsync();

                KPIResultDto result = _mapper.Map<KPIResultDto>(resultEntity);
                return new Response<KPIResultDto>(result, "", false);
            }
            catch (Exception ex)
            {
                return new Response<KPIResultDto>(default!, ex.Message, true);
            }
        }

        // ===================== Feedback =====================
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

                FeedbackDto result = _mapper.Map<FeedbackDto>(entity);
                return new Response<FeedbackDto>(result, "", false);
            }
            catch (Exception ex)
            {
                return new Response<FeedbackDto>(default!, ex.Message, true);
            }
        }

        public async Task<Response<IEnumerable<FeedbackDto>>> GetFeedbackByTypeAsync(int reviewId, FeedbackType type)
        {
            try
            {
                IEnumerable<Feedback> feedbacks = await _uow.Feedbacks.GetByReviewIdAndTypeAsync(reviewId, type);
                IEnumerable<FeedbackDto> result = _mapper.Map<IEnumerable<FeedbackDto>>(feedbacks);

                return new Response<IEnumerable<FeedbackDto>>(result, "", false);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<FeedbackDto>>(Array.Empty<FeedbackDto>(), ex.Message, true);
            }
        }


        // ===================== Competencies =====================
        public async Task<Response<CompetencyDto>> CreateCompetencyAsync(CreateCompetencyDto dto)
        {
            try
            {
                bool exists = await _uow.Competencies.ExistsByNameAsync(dto.Name);
                if (exists)
                    return new Response<CompetencyDto>(default!, "Competency with the same name already exists", true);

                Competency entity = _mapper.Map<Competency>(dto);
                await _uow.Competencies.AddAsync(entity);
                await _uow.SaveChangesAsync();

                CompetencyDto result = _mapper.Map<CompetencyDto>(entity);
                return new Response<CompetencyDto>(result, "", false);
            }
            catch (Exception ex)
            {
                return new Response<CompetencyDto>(default!, ex.Message, true);
            }
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

                EmployeeCompetencyRatingDto result = _mapper.Map<EmployeeCompetencyRatingDto>(rating);
                return new Response<EmployeeCompetencyRatingDto>(result, "", false);
            }
            catch (Exception ex)
            {
                return new Response<EmployeeCompetencyRatingDto>(default!, ex.Message, true);
            }
        }

        // ===================== Report =====================
        public async Task<Response<PerformanceReportDto>> GetReviewReportAsync(int reviewId)
        {
            try
            {
                PerformanceReview? review = await _uow.PerformanceReviews.GetByIdAsync(reviewId);
                if (review is null)
                    return new Response<PerformanceReportDto>(default!, "Review not found", true);

                IEnumerable<Goal> goals = await _uow.Goals.GetByReviewIdAsync(reviewId);
                IEnumerable<KPIResult> kpiRes = await _uow.KPIResults.GetByReviewIdAsync(reviewId);
                IEnumerable<EmployeeCompetencyRating> comps = await _uow.EmployeeCompetencyRatings.GetByReviewIdAsync(reviewId);
                IEnumerable<Feedback> feeds = await _uow.Feedbacks.GetByReviewIdAsync(reviewId);

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
