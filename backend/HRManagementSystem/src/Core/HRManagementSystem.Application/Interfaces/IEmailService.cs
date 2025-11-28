using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.Email;

namespace HRManagementSystem.Application.Interfaces;

public interface IEmailService
{
    Task<Response<bool>> SendEmailAsync(EmailDto emailDto, CancellationToken cancellationToken = default);
    Task<Response<bool>> SendWelcomeEmailAsync(string recipientEmail, string recipientName, CancellationToken cancellationToken = default);
    Task<Response<bool>> SendLeaveApprovalEmailAsync(string recipientEmail, string employeeName, string leaveType, string status, CancellationToken cancellationToken = default);
    Task<Response<bool>> SendPasswordResetEmailAsync(string recipientEmail, string resetToken, CancellationToken cancellationToken = default);
    Task<Response<bool>> SendJobApplicationStatusEmailAsync(string recipientEmail, string candidateName, string jobTitle, string status, CancellationToken cancellationToken = default);
    Task<Response<bool>> SendPayslipEmailAsync(string recipientEmail, string employeeName, string period, decimal netSalary, CancellationToken cancellationToken = default);
}
