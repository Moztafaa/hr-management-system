using HRManagementSystem.Application.DTOs.Email;
using HRManagementSystem.Application.DTOs.Notification;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domain.Entities;
using HRManagementSystem.Domain.Enums;
using HRManagementSystem.Domain.Enums.Notification;
using HRManagementSystem.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace HRManagementSystem.Infrastructure.Jobs;

/// <summary>
/// Background job for sending attendance-related reminders and notifications.
/// Uses Hangfire for scheduling daily check-in and check-out reminders.
/// </summary>
public class AttendanceReminderJob(
    IUnitOfWork unitOfWork,
    INotificationService notificationService,
    IEmailService emailService,
    ILogger<AttendanceReminderJob> logger) : IAttendanceReminderJob
{
    /// <inheritdoc />
    public async Task SendCheckInRemindersAsync()
    {
        logger.LogInformation("Starting check-in reminder job at {Time}", DateTime.Now);

        try
        {
            // Get all active employees
            IEnumerable<Employee> allEmployees = await unitOfWork.Employees.GetAllAsync();
            DateTime today = DateTime.Today;
            var employeesNotCheckedIn = new List<Employee>();

            foreach (Employee employee in allEmployees)
            {
                // Skip employees without effective dates or terminated employees
                if (employee.EFF_End.HasValue && employee.EFF_End.Value < today)
                {
                    continue;
                }

                // Check if employee has checked in today
                Attendance? todayAttendance = await unitOfWork.Attendances.GetTodayAttendanceAsync(employee.Id, today);

                if (todayAttendance == null || todayAttendance.CheckInTime == null)
                {
                    employeesNotCheckedIn.Add(employee);
                }
            }

            logger.LogInformation("Found {Count} employees who haven't checked in yet", employeesNotCheckedIn.Count);

            // Send notifications and emails to each employee
            foreach (Employee employee in employeesNotCheckedIn)
            {
                await SendCheckInReminderToEmployeeAsync(employee);
            }

            logger.LogInformation("Check-in reminder job completed. Sent {Count} reminders", employeesNotCheckedIn.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while sending check-in reminders");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task SendCheckOutRemindersAsync()
    {
        logger.LogInformation("Starting check-out reminder job at {Time}", DateTime.Now);

        try
        {
            // Get all active employees
            IEnumerable<Employee> allEmployees = await unitOfWork.Employees.GetAllAsync();
            DateTime today = DateTime.Today;
            var employeesNotCheckedOut = new List<Employee>();

            foreach (Employee employee in allEmployees)
            {
                // Skip employees without effective dates or terminated employees
                if (employee.EFF_End.HasValue && employee.EFF_End.Value < today)
                {
                    continue;
                }

                // Check if employee has checked in but not checked out
                Attendance? todayAttendance = await unitOfWork.Attendances.GetTodayAttendanceAsync(employee.Id, today);

                if (todayAttendance != null && todayAttendance.CheckInTime != null && todayAttendance.CheckOutTime == null)
                {
                    employeesNotCheckedOut.Add(employee);
                }
            }

            logger.LogInformation("Found {Count} employees who haven't checked out yet", employeesNotCheckedOut.Count);

            // Send notifications and emails to each employee
            foreach (Employee employee in employeesNotCheckedOut)
            {
                await SendCheckOutReminderToEmployeeAsync(employee);
            }

            logger.LogInformation("Check-out reminder job completed. Sent {Count} reminders", employeesNotCheckedOut.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while sending check-out reminders");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task SendAbsentEmployeesSummaryAsync()
    {
        logger.LogInformation("Starting absent employees summary job at {Time}", DateTime.Now);

        try
        {
            // Get all active employees
            IEnumerable<Employee> allEmployees = await unitOfWork.Employees.GetAllAsync();
            DateTime today = DateTime.Today;
            var absentEmployees = new List<Employee>();

            foreach (Employee employee in allEmployees)
            {
                // Skip employees without effective dates or terminated employees
                if (employee.EFF_End.HasValue && employee.EFF_End.Value < today)
                {
                    continue;
                }

                // Check if employee has checked in today
                Attendance? todayAttendance = await unitOfWork.Attendances.GetTodayAttendanceAsync(employee.Id, today);

                if (todayAttendance == null || todayAttendance.CheckInTime == null)
                {
                    absentEmployees.Add(employee);
                }
            }

            if (absentEmployees.Count == 0)
            {
                logger.LogInformation("All employees have checked in. No absent employees summary needed.");
                return;
            }

            // Build the summary email
            string emailBody = $@"
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .header {{ background-color: #dc3545; color: white; padding: 20px; text-align: center; }}
        .content {{ padding: 20px; }}
        .employee-list {{ background-color: #f8f9fa; padding: 15px; border-radius: 5px; margin: 15px 0; }}
        .footer {{ background-color: #f8f9fa; padding: 15px; text-align: center; font-size: 12px; color: #666; }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>⚠️ Daily Absent Employees Summary</h2>
    </div>
    <div class='content'>
        <p>Good morning,</p>
        <p>The following <strong>{absentEmployees.Count}</strong> employee(s) have not checked in as of {DateTime.Now:hh:mm tt}:</p>
        <div class='employee-list'>
            <ul>
                {string.Join("", absentEmployees.Select(e => $"<li><strong>{e.FirstName} {e.LastName}</strong> - {e.Email}</li>"))}
            </ul>
        </div>
        <p>Please follow up with these employees as needed.</p>
    </div>
    <div class='footer'>
        <p>This is an automated message from the HR Management System.</p>
    </div>
</body>
</html>";

            // Send to HR (you might want to configure this email address)
            EmailDto hrEmail = new()
            {
                To = "hr@company.com", // TODO: Make this configurable
                Subject = $"Daily Absent Employees Summary - {today:MMMM dd, yyyy}",
                Body = emailBody,
                IsHtml = true
            };

            await emailService.SendEmailAsync(hrEmail);

            logger.LogInformation("Absent employees summary sent. {Count} employees absent", absentEmployees.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while sending absent employees summary");
            throw;
        }
    }

    private async Task SendCheckInReminderToEmployeeAsync(Employee employee)
    {
        try
        {
            // Create in-app notification
            if (!string.IsNullOrEmpty(employee.ApplicationUserId))
            {
                CreateNotificationDto notification = new()
                {
                    RecipientUserId = employee.ApplicationUserId,
                    Title = "⏰ Check-In Reminder",
                    Message = $"Good morning, {employee.FirstName}! Don't forget to check in for today. Please use the attendance system to record your arrival.",
                    Type = NotificationType.Warning,
                    Category = NotificationCategory.AttendanceReminder,
                    Priority = NotificationPriority.Normal,
                    ActionUrl = "/attendance/check-in"
                };

                await notificationService.CreateNotificationAsync(notification);
            }

            // Send email reminder
            EmailDto emailDto = new()
            {
                To = employee.Email,
                Subject = "⏰ Daily Check-In Reminder - HR Management System",
                Body = GenerateCheckInEmailBody(employee),
                IsHtml = true
            };

            await emailService.SendEmailAsync(emailDto);

            logger.LogDebug("Check-in reminder sent to {EmployeeName} ({Email})",
                $"{employee.FirstName} {employee.LastName}", employee.Email);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to send check-in reminder to employee {EmployeeId}", employee.Id);
            // Don't throw - continue with other employees
        }
    }

    private async Task SendCheckOutReminderToEmployeeAsync(Employee employee)
    {
        try
        {
            // Create in-app notification
            if (!string.IsNullOrEmpty(employee.ApplicationUserId))
            {
                CreateNotificationDto notification = new()
                {
                    RecipientUserId = employee.ApplicationUserId,
                    Title = "🚪 Check-Out Reminder",
                    Message = $"Hi {employee.FirstName}, the workday is ending soon. Please remember to check out before leaving.",
                    Type = NotificationType.Info,
                    Category = NotificationCategory.AttendanceReminder,
                    Priority = NotificationPriority.Normal,
                    ActionUrl = "/attendance/check-out"
                };

                await notificationService.CreateNotificationAsync(notification);
            }

            // Send email reminder
            EmailDto emailDto = new()
            {
                To = employee.Email,
                Subject = "🚪 Daily Check-Out Reminder - HR Management System",
                Body = GenerateCheckOutEmailBody(employee),
                IsHtml = true
            };

            await emailService.SendEmailAsync(emailDto);

            logger.LogDebug("Check-out reminder sent to {EmployeeName} ({Email})",
                $"{employee.FirstName} {employee.LastName}", employee.Email);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to send check-out reminder to employee {EmployeeId}", employee.Id);
            // Don't throw - continue with other employees
        }
    }

    private static string GenerateCheckInEmailBody(Employee employee)
    {
        return $@"
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .header {{ background-color: #007bff; color: white; padding: 20px; text-align: center; }}
        .content {{ padding: 20px; }}
        .button {{ display: inline-block; padding: 12px 24px; background-color: #28a745; color: white; text-decoration: none; border-radius: 5px; margin-top: 15px; }}
        .footer {{ background-color: #f8f9fa; padding: 15px; text-align: center; font-size: 12px; color: #666; }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>⏰ Daily Check-In Reminder</h2>
    </div>
    <div class='content'>
        <p>Good morning, <strong>{employee.FirstName}</strong>!</p>
        <p>This is a friendly reminder to check in for today, <strong>{DateTime.Today:dddd, MMMM dd, yyyy}</strong>.</p>
        <p>Please use the HR Management System to record your attendance.</p>
        <p>Regular attendance tracking helps maintain accurate records and ensures compliance with company policies.</p>
        <p>Have a productive day! 🌟</p>
    </div>
    <div class='footer'>
        <p>This is an automated message from the HR Management System.</p>
        <p>If you have already checked in, please disregard this message.</p>
    </div>
</body>
</html>";
    }

    private static string GenerateCheckOutEmailBody(Employee employee)
    {
        return $@"
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .header {{ background-color: #6c757d; color: white; padding: 20px; text-align: center; }}
        .content {{ padding: 20px; }}
        .button {{ display: inline-block; padding: 12px 24px; background-color: #17a2b8; color: white; text-decoration: none; border-radius: 5px; margin-top: 15px; }}
        .footer {{ background-color: #f8f9fa; padding: 15px; text-align: center; font-size: 12px; color: #666; }}
    </style>
</head>
<body>
    <div class='header'>
        <h2>🚪 Daily Check-Out Reminder</h2>
    </div>
    <div class='content'>
        <p>Hi <strong>{employee.FirstName}</strong>,</p>
        <p>The workday is coming to an end. This is a friendly reminder to check out before leaving.</p>
        <p>Please use the HR Management System to record your departure time.</p>
        <p>Thank you for your hard work today! See you tomorrow. 👋</p>
    </div>
    <div class='footer'>
        <p>This is an automated message from the HR Management System.</p>
        <p>If you have already checked out, please disregard this message.</p>
    </div>
</body>
</html>";
    }
}
