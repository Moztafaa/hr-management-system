namespace HRManagementSystem.Application.Interfaces;

/// <summary>
/// Service interface for queuing emails to be sent asynchronously via background jobs.
/// This allows email sending to be decoupled from the main request thread.
/// </summary>
public interface IEmailQueueJob
{
    /// <summary>
    /// Sends a generic email asynchronously.
    /// </summary>
    Task SendEmailAsync(string to, string subject, string body, bool isHtml = true);

    /// <summary>
    /// Sends a welcome email to a new employee asynchronously.
    /// </summary>
    Task SendWelcomeEmailAsync(string recipientEmail, string recipientName);

    /// <summary>
    /// Sends a leave approval/rejection email asynchronously.
    /// </summary>
    Task SendLeaveApprovalEmailAsync(string recipientEmail, string employeeName, string leaveType, string status);

    /// <summary>
    /// Sends a password reset email asynchronously.
    /// </summary>
    Task SendPasswordResetEmailAsync(string recipientEmail, string resetToken);

    /// <summary>
    /// Sends a job application status email asynchronously.
    /// </summary>
    Task SendJobApplicationStatusEmailAsync(string recipientEmail, string candidateName, string jobTitle, string status);

    /// <summary>
    /// Sends a payslip notification email asynchronously.
    /// </summary>
    Task SendPayslipEmailAsync(string recipientEmail, string employeeName, string period, decimal netSalary);

    /// <summary>
    /// Sends a resignation notification email asynchronously.
    /// </summary>
    Task SendResignationNotificationEmailAsync(string recipientEmail, string recipientName, string employeeName, string status, DateTime lastWorkingDate);
}
