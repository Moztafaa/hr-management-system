using Hangfire;
using HRManagementSystem.Application.DTOs.Notification;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums.Notification;
using HRManagementSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HRManagementSystem.Infrastructure.Jobs;

/// <summary>
/// Background job for generating payslips for all employees.
/// Handles batch payslip generation with individual job queuing for each employee.
/// </summary>
public class PayslipGenerationJob(
    IUnitOfWork unitOfWork,
    IPayslipService payslipService,
    INotificationService notificationService,
    IBackgroundJobClient backgroundJobClient,
    ILogger<PayslipGenerationJob> logger) : IPayslipGenerationJob
{
    /// <inheritdoc />
    public async Task GenerateAllPayslipsAsync(int month, int year)
    {
        logger.LogInformation("Starting batch payslip generation for {Month}/{Year}", month, year);

        try
        {
            // Get all active employees
            IEnumerable<Employee> employees = await unitOfWork.Employees.GetAllAsync();
            DateTime today = DateTime.Today;

            var activeEmployees = employees
                .Where(e => !e.EFF_End.HasValue || e.EFF_End.Value >= today)
                .ToList();

            logger.LogInformation("Found {Count} active employees for payslip generation", activeEmployees.Count);

            int successCount = 0;
            int failCount = 0;

            foreach (Employee employee in activeEmployees)
            {
                try
                {
                    // Queue individual payslip generation as a separate background job
                    // This allows parallel processing and better fault isolation
                    backgroundJobClient.Enqueue<IPayslipGenerationJob>(
                        job => job.GeneratePayslipForEmployeeAsync(employee.Id, month, year));

                    successCount++;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to queue payslip generation for employee {EmployeeId}", employee.Id);
                    failCount++;
                }
            }

            logger.LogInformation(
                "Batch payslip generation queued. Success: {SuccessCount}, Failed: {FailCount}",
                successCount, failCount);

            // Create summary notification for HR
            await notificationService.CreateNotificationAsync(new CreateNotificationDto
            {
                RecipientUserId = null, // System-wide notification
                Title = "📊 Payslip Generation Started",
                Message = $"Payslip generation for {month}/{year} has been initiated for {successCount} employees.",
                Type = NotificationType.Info,
                Category = NotificationCategory.Payroll,
                Priority = NotificationPriority.Normal
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred during batch payslip generation for {Month}/{Year}", month, year);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task GeneratePayslipForEmployeeAsync(int employeeId, int month, int year)
    {
        logger.LogInformation("Generating payslip for employee {EmployeeId} for {Month}/{Year}", employeeId, month, year);

        try
        {
            // Generate the payslip using the existing service
            var result = await payslipService.GeneratePayslipAsync(employeeId, month, year, CancellationToken.None);

            if (result.HasError)
            {
                logger.LogWarning("Failed to generate payslip for employee {EmployeeId}: {Error}",
                    employeeId, result.ErrorMessage);
                return;
            }

            logger.LogInformation("Payslip generated successfully for employee {EmployeeId}. Net Salary: {NetSalary}",
                employeeId, result.Data.NetSalary);

            // Queue the notification sending as a continuation job
            backgroundJobClient.Enqueue<IPayslipGenerationJob>(
                job => job.SendPayslipNotificationAsync(
                    employeeId,
                    result.Data.Id,
                    month,
                    year,
                    result.Data.NetSalary));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error generating payslip for employee {EmployeeId}", employeeId);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task SendPayslipNotificationAsync(int employeeId, int payslipId, int month, int year, decimal netSalary)
    {
        logger.LogInformation("Sending payslip notification for employee {EmployeeId}", employeeId);

        try
        {
            Employee? employee = await unitOfWork.Employees.GetByIdAsync(employeeId);
            if (employee == null)
            {
                logger.LogWarning("Employee {EmployeeId} not found for payslip notification", employeeId);
                return;
            }

            // Create in-app notification
            if (!string.IsNullOrEmpty(employee.ApplicationUserId))
            {
                await notificationService.CreateNotificationAsync(new CreateNotificationDto
                {
                    RecipientUserId = employee.ApplicationUserId,
                    Title = "💰 Payslip Generated",
                    Message = $"Your payslip for {month}/{year} has been generated. Net Salary: ${netSalary:N2}",
                    Type = NotificationType.Success,
                    Category = NotificationCategory.Payroll,
                    Priority = NotificationPriority.Normal,
                    RelatedEntityId = payslipId,
                    RelatedEntityType = "Payslip",
                    ActionUrl = $"/ess/payslips/{payslipId}"
                });
            }

            logger.LogInformation("Payslip notification sent to employee {EmployeeId}", employeeId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send payslip notification to employee {EmployeeId}", employeeId);
            // Don't throw - notification failure shouldn't fail the entire job
        }
    }
}
