using Hangfire;
using HRManagementSystem.Application.DTOs.Email;
using HRManagementSystem.Application.DTOs.Notification;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Application.Interfaces.ILeaveServices;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums.Notification;
using HRManagementSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HRManagementSystem.Infrastructure.Jobs;

/// <summary>
/// Background job for resetting/allocating leave balances for all employees.
/// Typically scheduled to run on January 1st of each year.
/// </summary>
public class LeaveBalanceResetJob(
    IUnitOfWork unitOfWork,
    ILeaveBalanceService leaveBalanceService,
    INotificationService notificationService,
    IEmailService emailService,
    IBackgroundJobClient backgroundJobClient,
    ILogger<LeaveBalanceResetJob> logger) : ILeaveBalanceResetJob
{
    /// <inheritdoc />
    public async Task AllocateLeaveBalancesForNewYearAsync()
    {
        int currentYear = DateTime.UtcNow.Year;
        logger.LogInformation("Starting leave balance allocation for year {Year}", currentYear);

        try
        {
            // Get all active employees
            IEnumerable<Employee> employees = await unitOfWork.Employees.GetAllAsync();
            DateTime today = DateTime.Today;

            var activeEmployees = employees
                .Where(e => !e.EFF_End.HasValue || e.EFF_End.Value >= today)
                .ToList();

            logger.LogInformation("Found {Count} active employees for leave balance allocation", activeEmployees.Count);

            int successCount = 0;
            int failCount = 0;

            foreach (Employee employee in activeEmployees)
            {
                try
                {
                    // Queue individual allocation as a separate background job
                    backgroundJobClient.Enqueue<ILeaveBalanceResetJob>(
                        job => job.AllocateLeaveBalanceForEmployeeAsync(employee.Id));

                    successCount++;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to queue leave balance allocation for employee {EmployeeId}", employee.Id);
                    failCount++;
                }
            }

            logger.LogInformation(
                "Leave balance allocation queued. Total: {Total}, Queued: {SuccessCount}, Failed: {FailCount}",
                activeEmployees.Count, successCount, failCount);

            // Schedule summary notification after a delay to allow processing
            backgroundJobClient.Schedule<ILeaveBalanceResetJob>(
                job => job.SendLeaveBalanceResetSummaryAsync(successCount, failCount),
                TimeSpan.FromMinutes(5));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred during leave balance allocation for year {Year}", currentYear);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task AllocateLeaveBalanceForEmployeeAsync(int employeeId)
    {
        logger.LogInformation("Allocating leave balances for employee {EmployeeId}", employeeId);

        try
        {
            var result = await leaveBalanceService.AllocateInitialBalancesAsync(employeeId);

            if (result.HasError)
            {
                logger.LogWarning("Failed to allocate leave balance for employee {EmployeeId}: {Error}",
                    employeeId, result.ErrorMessage);
                return;
            }

            // Send notification to employee about new leave allocation
            Employee? employee = await unitOfWork.Employees.GetByIdAsync(employeeId);
            if (employee != null && !string.IsNullOrEmpty(employee.ApplicationUserId))
            {
                await notificationService.CreateNotificationAsync(new CreateNotificationDto
                {
                    RecipientUserId = employee.ApplicationUserId,
                    Title = "🎉 New Year Leave Allocation",
                    Message = $"Your leave balances for {DateTime.UtcNow.Year} have been allocated. Check your leave balance to see your available days.",
                    Type = NotificationType.Success,
                    Category = NotificationCategory.LeaveRequest,
                    Priority = NotificationPriority.Normal,
                    ActionUrl = "/ess/leave-balance"
                });
            }

            logger.LogInformation("Leave balances allocated successfully for employee {EmployeeId}", employeeId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error allocating leave balance for employee {EmployeeId}", employeeId);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task SendLeaveBalanceResetSummaryAsync(int successCount, int failCount)
    {
        int currentYear = DateTime.UtcNow.Year;
        logger.LogInformation("Sending leave balance reset summary for year {Year}", currentYear);

        try
        {
            // Create system notification for HR
            await notificationService.CreateNotificationAsync(new CreateNotificationDto
            {
                RecipientUserId = null, // System-wide notification
                Title = "📋 Leave Balance Allocation Complete",
                Message = $"Leave balance allocation for {currentYear} has been completed. Processed: {successCount}, Failed: {failCount}",
                Type = failCount > 0 ? NotificationType.Warning : NotificationType.Success,
                Category = NotificationCategory.System,
                Priority = failCount > 0 ? NotificationPriority.High : NotificationPriority.Normal
            });

            // Send summary email to HR
            string emailBody = $@"
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .header {{ background-color: #28a745; color: white; padding: 20px; text-align: center; }}
        .header.warning {{ background-color: #ffc107; color: #333; }}
        .content {{ padding: 20px; }}
        .summary {{ background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 15px 0; }}
        .footer {{ background-color: #f8f9fa; padding: 15px; text-align: center; font-size: 12px; color: #666; }}
    </style>
</head>
<body>
    <div class='header{(failCount > 0 ? " warning" : "")}'>
        <h2>📋 Leave Balance Allocation Summary - {currentYear}</h2>
    </div>
    <div class='content'>
        <p>The annual leave balance allocation process has been completed.</p>
        <div class='summary'>
            <h3>Summary:</h3>
            <ul>
                <li><strong>Year:</strong> {currentYear}</li>
                <li><strong>Successfully Processed:</strong> {successCount} employees</li>
                <li><strong>Failed:</strong> {failCount} employees</li>
                <li><strong>Completion Time:</strong> {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC</li>
            </ul>
        </div>
        {(failCount > 0 ? "<p><strong>⚠️ Note:</strong> Some allocations failed. Please check the system logs for details.</p>" : "")}
        <p>All employees have been notified about their new leave balances.</p>
    </div>
    <div class='footer'>
        <p>This is an automated message from the HR Management System.</p>
    </div>
</body>
</html>";

            EmailDto hrEmail = new()
            {
                To = "hr@company.com", // TODO: Make this configurable
                Subject = $"Leave Balance Allocation Complete - {currentYear}",
                Body = emailBody,
                IsHtml = true
            };

            await emailService.SendEmailAsync(hrEmail);

            logger.LogInformation("Leave balance reset summary sent. Success: {SuccessCount}, Failed: {FailCount}",
                successCount, failCount);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send leave balance reset summary");
            // Don't throw - summary notification failure shouldn't fail
        }
    }
}
