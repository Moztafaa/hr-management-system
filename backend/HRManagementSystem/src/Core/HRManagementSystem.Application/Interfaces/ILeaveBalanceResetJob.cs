namespace HRManagementSystem.Application.Interfaces;

/// <summary>
/// Service interface for leave balance reset background jobs.
/// Handles annual leave balance allocation for all employees.
/// </summary>
public interface ILeaveBalanceResetJob
{
    /// <summary>
    /// Allocates leave balances for all active employees for the new year.
    /// This is a long-running operation that should be executed as a scheduled background job.
    /// Typically scheduled to run on January 1st of each year.
    /// </summary>
    Task AllocateLeaveBalancesForNewYearAsync();

    /// <summary>
    /// Allocates leave balances for a single employee.
    /// Useful for queuing individual allocations as separate background jobs.
    /// </summary>
    /// <param name="employeeId">The employee ID to allocate balances for.</param>
    Task AllocateLeaveBalanceForEmployeeAsync(int employeeId);

    /// <summary>
    /// Sends a notification to HR about the leave balance reset completion.
    /// </summary>
    /// <param name="successCount">Number of employees successfully processed.</param>
    /// <param name="failCount">Number of employees that failed processing.</param>
    Task SendLeaveBalanceResetSummaryAsync(int successCount, int failCount);
}
