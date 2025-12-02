namespace HRManagementSystem.Application.Interfaces;

/// <summary>
/// Service interface for attendance-related background jobs.
/// Handles reminders and notifications for employee attendance.
/// </summary>
public interface IAttendanceReminderJob
{
    /// <summary>
    /// Sends check-in reminder notifications to all active employees who haven't checked in yet today.
    /// Typically scheduled to run in the morning (e.g., 8:30 AM).
    /// </summary>
    Task SendCheckInRemindersAsync();

    /// <summary>
    /// Sends check-out reminder notifications to all employees who have checked in but haven't checked out.
    /// Typically scheduled to run in the evening (e.g., 5:30 PM).
    /// </summary>
    Task SendCheckOutRemindersAsync();

    /// <summary>
    /// Sends a summary email of absent employees to HR/managers.
    /// Typically scheduled to run mid-morning (e.g., 10:00 AM).
    /// </summary>
    Task SendAbsentEmployeesSummaryAsync();
}
