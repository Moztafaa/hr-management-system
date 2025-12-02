using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.Chat;
using HRManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChatController(IRAGService ragService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AskQuestion([FromBody] ChatRequestDto request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Question))
        {
            return BadRequest(new { hasError = true, errorMessage = "Question cannot be empty" });
        }

        Response<ChatResponseDto> response = await ragService.AskQuestionAsync(request.Question, cancellationToken);

        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }

        return Ok(response);
    }

    [HttpPost("add-context")]
    public async Task<IActionResult> AddContext([FromBody] AddContextDto request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest(new { hasError = true, errorMessage = "Text cannot be empty" });
        }

        Response<bool> response = await ragService.AddContextAsync(request.EntityType, request.Text, cancellationToken);

        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }

        return Ok(response);
    }

    /// <summary>
    /// Syncs all existing employees from the database to the RAG vector store.
    /// Call this once to populate the AI with existing employee data.
    /// </summary>
    [HttpPost("sync-employees")]
    public async Task<IActionResult> SyncEmployees(CancellationToken cancellationToken)
    {
        Response<int> response = await ragService.SyncAllEmployeesAsync(cancellationToken);

        if (response.HasError)
        {
            return BadRequest(new { hasError = response.HasError, errorMessage = response.ErrorMessage });
        }

        return Ok(new { hasError = false, message = $"Successfully synced {response.Data} employees to RAG vector store" });
    }
}

public class AddContextDto
{
    public string EntityType { get; set; } = "General";
    public required string Text { get; set; }
}
