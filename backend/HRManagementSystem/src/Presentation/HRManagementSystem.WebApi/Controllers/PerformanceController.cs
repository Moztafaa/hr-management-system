using HRManagementSystem.Application.DTOs.Performance;
using HRManagementSystem.Domain.Enums.Performance;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PerformanceController : ControllerBase
{
    private readonly IPerformanceService _performanceService;

    public PerformanceController(IPerformanceService performanceService)
    {
        _performanceService = performanceService;
    }

    // ========================= Cycles =========================

    [HttpPost("cycles")]
    public async Task<IActionResult> CreateCycle([FromBody] CreateCycleDto dto)
    {
        Response<ReviewCycleDto> result = await _performanceService.CreateCycleAsync(dto);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpGet("cycles")]
    public async Task<IActionResult> GetCycles()
    {
        Response<IEnumerable<ReviewCycleDto>> result = await _performanceService.GetCyclesAsync();
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpGet("cycles/active")]
    public async Task<IActionResult> GetActiveCycles([FromQuery] DateTime? at)
    {
        var result = await _performanceService.GetActiveCyclesAsync(at);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }
    // ========================= Reviews =========================

    [HttpPost("reviews")]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto dto)
    {
        Response<PerformanceReviewDto> result = await _performanceService.CreateReviewAsync(dto);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpPost("reviews/{reviewId}/close")]
    public async Task<IActionResult> CloseReview([FromRoute] int reviewId, [FromBody] decimal? finalRating)
    {
        Response<PerformanceReviewDto> result = await _performanceService.CloseReviewAsync(reviewId, finalRating);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpGet("reviews/employee/{employeeId:int}")]
    public async Task<IActionResult> GetEmployeeReviews([FromRoute] int employeeId)
    {
        var result = await _performanceService.GetEmployeeReviewsAsync(employeeId);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }
    // ========================= Goals =========================

    [HttpPost("goals")]
    public async Task<IActionResult> CreateGoal([FromBody] CreateGoalDto dto)
    {
        Response<GoalDto> result = await _performanceService.CreateGoalAsync(dto);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpPut("goals/progress")]
    public async Task<IActionResult> UpdateGoalProgress([FromBody] UpdateGoalProgressDto dto)
    {
        Response<bool> result = await _performanceService.UpdateGoalProgressAsync(dto);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    // ========================= KPIs =========================

    [HttpPost("kpis")]
    public async Task<IActionResult> CreateKpi([FromBody] CreateKpiDto dto)
    {
        Response<KPIDto> result = await _performanceService.CreateKpiAsync(dto);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpPost("kpis/results")]
    public async Task<IActionResult> AddKpiResult([FromBody] CreateKpiResultDto dto)
    {
        Response<KPIResultDto> result = await _performanceService.AddKpiResultAsync(dto);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    // ========================= Feedback =========================

    [HttpPost("feedbacks")]
    public async Task<IActionResult> AddFeedback([FromBody] CreateFeedbackDto dto)
    {
        Response<FeedbackDto> result = await _performanceService.AddFeedbackAsync(dto);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpGet("reviews/{reviewId:int}/feedback-by-type")]
    public async Task<IActionResult> GetFeedbackByType(int reviewId,[FromQuery] FeedbackType type)
    {
        Response<IEnumerable<FeedbackDto>> result = await _performanceService.GetFeedbackByTypeAsync(reviewId, type);
        if (result.HasError)
            return BadRequest(result);

        return Ok(result);
    }

    // ========================= Competencies =========================

    [HttpPost("competencies")]
    public async Task<IActionResult> CreateCompetency([FromBody] CreateCompetencyDto dto)
    {
        Response<CompetencyDto> result = await _performanceService.CreateCompetencyAsync(dto);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    [HttpPost("competencies/rate")]
    public async Task<IActionResult> RateCompetency([FromBody] RateCompetencyDto dto)
    {
        Response<EmployeeCompetencyRatingDto> result = await _performanceService.RateCompetencyAsync(dto);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }

    // ========================= Report =========================

    [HttpGet("reviews/{reviewId}/report")]
    public async Task<IActionResult> GetReviewReport([FromRoute] int reviewId)
    {
        Response<PerformanceReportDto> result = await _performanceService.GetReviewReportAsync(reviewId);
        if (result.HasError)
            return BadRequest(new { hasError = result.HasError, errorMessage = result.ErrorMessage });

        return Ok(new { hasError = result.HasError, data = result.Data });
    }
}
