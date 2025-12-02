using HRManagementSystem.Application.DTOs.Email;
using HRManagementSystem.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace HRManagementSystem.Infrastructure.Jobs;

/// <summary>
/// Background job for sending emails asynchronously.
/// This decouples email sending from the main request thread, improving API response times.
/// </summary>
public class EmailQueueJob(
    IEmailService emailService,
    ILogger<EmailQueueJob> logger) : IEmailQueueJob
{
    /// <inheritdoc />
    public async Task SendEmailAsync(string to, string subject, string body, bool isHtml = true)
    {
        logger.LogInformation("Sending email to {To} with subject: {Subject}", to, subject);

        try
        {
            EmailDto emailDto = new()
            {
                To = to,
                Subject = subject,
                Body = body,
                IsHtml = isHtml
            };

            await emailService.SendEmailAsync(emailDto);
            logger.LogInformation("Email sent successfully to {To}", to);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email to {To}", to);
            throw; // Re-throw to let Hangfire handle retry
        }
    }

    /// <inheritdoc />
    public async Task SendWelcomeEmailAsync(string recipientEmail, string recipientName)
    {
        logger.LogInformation("Sending welcome email to {Email}", recipientEmail);

        try
        {
            await emailService.SendWelcomeEmailAsync(recipientEmail, recipientName);
            logger.LogInformation("Welcome email sent successfully to {Email}", recipientEmail);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send welcome email to {Email}", recipientEmail);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task SendLeaveApprovalEmailAsync(string recipientEmail, string employeeName, string leaveType, string status)
    {
        logger.LogInformation("Sending leave {Status} email to {Email}", status, recipientEmail);

        try
        {
            await emailService.SendLeaveApprovalEmailAsync(recipientEmail, employeeName, leaveType, status);
            logger.LogInformation("Leave {Status} email sent successfully to {Email}", status, recipientEmail);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send leave {Status} email to {Email}", status, recipientEmail);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task SendPasswordResetEmailAsync(string recipientEmail, string resetToken)
    {
        logger.LogInformation("Sending password reset email to {Email}", recipientEmail);

        try
        {
            await emailService.SendPasswordResetEmailAsync(recipientEmail, resetToken);
            logger.LogInformation("Password reset email sent successfully to {Email}", recipientEmail);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send password reset email to {Email}", recipientEmail);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task SendJobApplicationStatusEmailAsync(string recipientEmail, string candidateName, string jobTitle, string status)
    {
        logger.LogInformation("Sending job application {Status} email to {Email}", status, recipientEmail);

        try
        {
            await emailService.SendJobApplicationStatusEmailAsync(recipientEmail, candidateName, jobTitle, status);
            logger.LogInformation("Job application {Status} email sent successfully to {Email}", status, recipientEmail);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send job application {Status} email to {Email}", status, recipientEmail);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task SendPayslipEmailAsync(string recipientEmail, string employeeName, string period, decimal netSalary)
    {
        logger.LogInformation("Sending payslip email to {Email} for period {Period}", recipientEmail, period);

        try
        {
            await emailService.SendPayslipEmailAsync(recipientEmail, employeeName, period, netSalary);
            logger.LogInformation("Payslip email sent successfully to {Email}", recipientEmail);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send payslip email to {Email}", recipientEmail);
            throw;
        }
    }
}
