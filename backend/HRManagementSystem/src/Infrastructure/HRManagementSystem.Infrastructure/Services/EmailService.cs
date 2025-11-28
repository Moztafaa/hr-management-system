using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.Email;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Infrastructure.Common;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HRManagementSystem.Infrastructure.Services;

public class EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger) : IEmailService
{
    private readonly EmailSettings _emailSettings = emailSettings.Value;

    public async Task<Response<bool>> SendEmailAsync(EmailDto emailDto, CancellationToken cancellationToken = default)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(emailDto.To));

            if (!string.IsNullOrEmpty(emailDto.Cc))
            {
                message.Cc.Add(MailboxAddress.Parse(emailDto.Cc));
            }

            if (!string.IsNullOrEmpty(emailDto.Bcc))
            {
                message.Bcc.Add(MailboxAddress.Parse(emailDto.Bcc));
            }

            message.Subject = emailDto.Subject;

            var bodyBuilder = new BodyBuilder();
            if (emailDto.IsHtml)
            {
                bodyBuilder.HtmlBody = emailDto.Body;
            }
            else
            {
                bodyBuilder.TextBody = emailDto.Body;
            }

            // Handle attachments if any
            if (emailDto.Attachments != null && emailDto.Attachments.Count > 0)
            {
                foreach (var attachmentPath in emailDto.Attachments)
                {
                    if (File.Exists(attachmentPath))
                    {
                        bodyBuilder.Attachments.Add(attachmentPath);
                    }
                }
            }

            message.Body = bodyBuilder.ToMessageBody();

            using var smtpClient = new SmtpClient();

            if (_emailSettings.EnableSsl)
            {
                await smtpClient.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls, cancellationToken);
            }
            else
            {
                await smtpClient.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.None, cancellationToken);
            }

            // Authenticate if credentials provided
            if (!string.IsNullOrEmpty(_emailSettings.Username) && !string.IsNullOrEmpty(_emailSettings.Password))
            {
                await smtpClient.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password, cancellationToken);
            }

            await smtpClient.SendAsync(message, cancellationToken);
            await smtpClient.DisconnectAsync(true, cancellationToken);

            logger.LogInformation("Email sent successfully to {To} with subject: {Subject}", emailDto.To, emailDto.Subject);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email to {To}", emailDto.To);
            return new Response<bool>(false, ex.Message, true);
        }
    }

    public async Task<Response<bool>> SendWelcomeEmailAsync(string recipientEmail, string recipientName, CancellationToken cancellationToken = default)
    {
        var emailBody = GetWelcomeEmailTemplate(recipientName);
        var emailDto = new EmailDto
        {
            To = recipientEmail,
            Subject = "Welcome to HR Management System!",
            Body = emailBody,
            IsHtml = true
        };

        return await SendEmailAsync(emailDto, cancellationToken);
    }

    public async Task<Response<bool>> SendLeaveApprovalEmailAsync(string recipientEmail, string employeeName, string leaveType, string status, CancellationToken cancellationToken = default)
    {
        var emailBody = GetLeaveApprovalEmailTemplate(employeeName, leaveType, status);
        var emailDto = new EmailDto
        {
            To = recipientEmail,
            Subject = $"Leave Request {status} - {leaveType}",
            Body = emailBody,
            IsHtml = true
        };

        return await SendEmailAsync(emailDto, cancellationToken);
    }

    public async Task<Response<bool>> SendPasswordResetEmailAsync(string recipientEmail, string resetToken, CancellationToken cancellationToken = default)
    {
        var emailBody = GetPasswordResetEmailTemplate(resetToken);
        var emailDto = new EmailDto
        {
            To = recipientEmail,
            Subject = "Password Reset Request",
            Body = emailBody,
            IsHtml = true
        };

        return await SendEmailAsync(emailDto, cancellationToken);
    }

    public async Task<Response<bool>> SendJobApplicationStatusEmailAsync(string recipientEmail, string candidateName, string jobTitle, string status, CancellationToken cancellationToken = default)
    {
        var emailBody = GetJobApplicationStatusEmailTemplate(candidateName, jobTitle, status);
        var emailDto = new EmailDto
        {
            To = recipientEmail,
            Subject = $"Job Application Update - {jobTitle}",
            Body = emailBody,
            IsHtml = true
        };

        return await SendEmailAsync(emailDto, cancellationToken);
    }

    public async Task<Response<bool>> SendPayslipEmailAsync(string recipientEmail, string employeeName, string period, decimal netSalary, CancellationToken cancellationToken = default)
    {
        var emailBody = GetPayslipEmailTemplate(employeeName, period, netSalary);
        var emailDto = new EmailDto
        {
            To = recipientEmail,
            Subject = $"Payslip for {period}",
            Body = emailBody,
            IsHtml = true
        };

        return await SendEmailAsync(emailDto, cancellationToken);
    }

    #region Email Templates

    private string GetWelcomeEmailTemplate(string recipientName)
    {
        return $@"
            <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                    .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                    .header {{ background-color: #4CAF50; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }}
                    .content {{ background-color: #f9f9f9; padding: 30px; border-radius: 0 0 5px 5px; }}
                    .footer {{ margin-top: 20px; text-align: center; font-size: 12px; color: #777; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h1>Welcome to HR Management System!</h1>
                    </div>
                    <div class='content'>
                        <p>Dear {recipientName},</p>
                        <p>Your account has been successfully created in our HR Management System.</p>
                        <p>You can now access all the features and services available to you.</p>
                        <p>If you have any questions or need assistance, please don't hesitate to contact our HR team.</p>
                        <p>Best regards,<br/>HR Team</p>
                    </div>
                    <div class='footer'>
                        <p>This is an automated message from HR Management System. Please do not reply to this email.</p>
                    </div>
                </div>
            </body>
            </html>";
    }

    private string GetLeaveApprovalEmailTemplate(string employeeName, string leaveType, string status)
    {
        var statusColor = status.ToLower() switch
        {
            "approved" => "#4CAF50",
            "rejected" => "#f44336",
            _ => "#FF9800"
        };

        return $@"
            <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                    .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                    .header {{ background-color: {statusColor}; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }}
                    .content {{ background-color: #f9f9f9; padding: 30px; border-radius: 0 0 5px 5px; }}
                    .status {{ font-weight: bold; color: {statusColor}; font-size: 18px; }}
                    .footer {{ margin-top: 20px; text-align: center; font-size: 12px; color: #777; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h1>Leave Request Update</h1>
                    </div>
                    <div class='content'>
                        <p>Dear {employeeName},</p>
                        <p>Your leave request for <strong>{leaveType}</strong> has been <span class='status'>{status}</span>.</p>
                        {(status.ToLower() == "approved" ? "<p>Enjoy your time off!</p>" : "")}
                        {(status.ToLower() == "rejected" ? "<p>If you have any questions about this decision, please contact your manager.</p>" : "")}
                        {(status.ToLower() == "pending" ? "<p>Your request is awaiting approval from your manager.</p>" : "")}
                        <p>Best regards,<br/>HR Team</p>
                    </div>
                    <div class='footer'>
                        <p>This is an automated message from HR Management System. Please do not reply to this email.</p>
                    </div>
                </div>
            </body>
            </html>";
    }

    private string GetPasswordResetEmailTemplate(string resetToken)
    {
        return $@"
            <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                    .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                    .header {{ background-color: #2196F3; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }}
                    .content {{ background-color: #f9f9f9; padding: 30px; border-radius: 0 0 5px 5px; }}
                    .token {{ background-color: #fff; padding: 15px; border: 2px dashed #2196F3; margin: 20px 0; font-family: monospace; word-break: break-all; }}
                    .warning {{ background-color: #fff3cd; padding: 10px; border-left: 4px solid #ffc107; margin: 20px 0; }}
                    .footer {{ margin-top: 20px; text-align: center; font-size: 12px; color: #777; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h1>Password Reset Request</h1>
                    </div>
                    <div class='content'>
                        <p>You have requested to reset your password.</p>
                        <p>Please use the following token to reset your password:</p>
                        <div class='token'>{resetToken}</div>
                        <div class='warning'>
                            <strong>Security Notice:</strong> If you did not request a password reset, please ignore this email and ensure your account is secure.
                        </div>
                        <p>This token will expire in 24 hours.</p>
                        <p>Best regards,<br/>HR Team</p>
                    </div>
                    <div class='footer'>
                        <p>This is an automated message from HR Management System. Please do not reply to this email.</p>
                    </div>
                </div>
            </body>
            </html>";
    }

    private string GetJobApplicationStatusEmailTemplate(string candidateName, string jobTitle, string status)
    {
        var statusMessage = status.ToLower() switch
        {
            "applied" => "We have received your application and it is currently under review.",
            "underreview" => "Your application is currently being reviewed by our recruitment team.",
            "interviewscheduled" => "Congratulations! Your application has progressed to the interview stage. Our team will contact you shortly with interview details.",
            "offered" => "Congratulations! We are pleased to offer you the position. Our HR team will contact you with the offer details.",
            "rejected" => "Thank you for your interest in this position. Unfortunately, we have decided to move forward with other candidates. We encourage you to apply for future openings that match your qualifications.",
            "hired" => "Congratulations! Welcome to the team! Our HR department will be in touch with onboarding information.",
            _ => "Your application status has been updated."
        };

        return $@"
            <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                    .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                    .header {{ background-color: #673AB7; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }}
                    .content {{ background-color: #f9f9f9; padding: 30px; border-radius: 0 0 5px 5px; }}
                    .status {{ font-weight: bold; color: #673AB7; text-transform: uppercase; }}
                    .footer {{ margin-top: 20px; text-align: center; font-size: 12px; color: #777; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h1>Job Application Update</h1>
                    </div>
                    <div class='content'>
                        <p>Dear {candidateName},</p>
                        <p>Thank you for your interest in the <strong>{jobTitle}</strong> position.</p>
                        <p>Application Status: <span class='status'>{status}</span></p>
                        <p>{statusMessage}</p>
                        <p>Best regards,<br/>Recruitment Team</p>
                    </div>
                    <div class='footer'>
                        <p>This is an automated message from HR Management System. Please do not reply to this email.</p>
                    </div>
                </div>
            </body>
            </html>";
    }

    private string GetPayslipEmailTemplate(string employeeName, string period, decimal netSalary)
    {
        return $@"
            <html>
            <head>
                <style>
                    body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                    .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                    .header {{ background-color: #009688; color: white; padding: 20px; text-align: center; border-radius: 5px 5px 0 0; }}
                    .content {{ background-color: #f9f9f9; padding: 30px; border-radius: 0 0 5px 5px; }}
                    .payslip-info {{ background-color: #fff; padding: 20px; margin: 20px 0; border-radius: 5px; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }}
                    .amount {{ font-size: 24px; color: #009688; font-weight: bold; }}
                    .footer {{ margin-top: 20px; text-align: center; font-size: 12px; color: #777; }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h1>Payslip Notification</h1>
                    </div>
                    <div class='content'>
                        <p>Dear {employeeName},</p>
                        <p>Your payslip for <strong>{period}</strong> has been generated.</p>
                        <div class='payslip-info'>
                            <p><strong>Pay Period:</strong> {period}</p>
                            <p><strong>Net Salary:</strong> <span class='amount'>${netSalary:N2}</span></p>
                        </div>
                        <p>Please log in to the HR Management System to view your complete payslip details.</p>
                        <p>If you have any questions regarding your payslip, please contact the payroll department.</p>
                        <p>Best regards,<br/>Payroll Team</p>
                    </div>
                    <div class='footer'>
                        <p>This is an automated message from HR Management System. Please do not reply to this email.</p>
                    </div>
                </div>
            </body>
            </html>";
    }

    #endregion
}
