using Hangfire;
using HRManagementSystem.Application.Interfaces;

namespace HRManagementSystem.WebApi.Extensions;

/// <summary>
/// Extension methods for configuring Hangfire dashboard and recurring jobs.
/// </summary>
public static class HangfireExtensions
{
    /// <summary>
    /// Configures and uses the Hangfire dashboard.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <returns>The web application for method chaining.</returns>
    public static WebApplication UseHangfireDashboardWithConfig(this WebApplication app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = "HR Management System - Background Jobs",
            // For development, allow anonymous access
            // In production, use: Authorization = new[] { new HangfireAuthorizationFilter() }
            IsReadOnlyFunc = (context) => false
        });

        return app;
    }

    /// <summary>
    /// Registers all recurring background jobs for the HR Management System.
    /// </summary>
    /// <param name="app">The web application.</param>
    /// <returns>The web application for method chaining.</returns>
    public static WebApplication RegisterHangfireRecurringJobs(this WebApplication app)
    {
        RegisterAttendanceReminderJobs();
        RegisterPayrollJobs();
        RegisterLeaveManagementJobs();

        return app;
    }

    /// <summary>
    /// Registers attendance reminder related recurring jobs.
    /// </summary>
    private static void RegisterAttendanceReminderJobs()
    {
        // Daily check-in reminder at 8:30 AM (Monday to Friday)
        // Cron: "30 8 * * 1-5" = At 08:30, Monday through Friday
        RecurringJob.AddOrUpdate<IAttendanceReminderJob>(
            recurringJobId: "daily-checkin-reminder",
            methodCall: job => job.SendCheckInRemindersAsync(),
            cronExpression: "30 8 * * 1-5",
            options: new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });

        // Daily check-out reminder at 5:30 PM (Monday to Friday)
        // Cron: "30 17 * * 1-5" = At 17:30, Monday through Friday
        RecurringJob.AddOrUpdate<IAttendanceReminderJob>(
            recurringJobId: "daily-checkout-reminder",
            methodCall: job => job.SendCheckOutRemindersAsync(),
            cronExpression: "30 17 * * 1-5",
            options: new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });

        // Absent employees summary email at 10:00 AM (Monday to Friday)
        // Cron: "0 10 * * 1-5" = At 10:00, Monday through Friday
        RecurringJob.AddOrUpdate<IAttendanceReminderJob>(
            recurringJobId: "absent-employees-summary",
            methodCall: job => job.SendAbsentEmployeesSummaryAsync(),
            cronExpression: "0 10 * * 1-5",
            options: new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });
    }

    /// <summary>
    /// Registers payroll related recurring jobs.
    /// </summary>
    private static void RegisterPayrollJobs()
    {
        // Monthly payslip generation on the 1st of each month at 6:00 AM
        // Cron: "0 6 1 * *" = At 06:00 on the 1st of every month
        RecurringJob.AddOrUpdate<IPayslipGenerationJob>(
            recurringJobId: "monthly-payslip-generation",
            methodCall: job => job.GenerateAllPayslipsAsync(
                DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1,
                DateTime.Now.Month == 1 ? DateTime.Now.Year - 1 : DateTime.Now.Year),
            cronExpression: "0 6 1 * *",
            options: new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });
    }

    /// <summary>
    /// Registers leave management related recurring jobs.
    /// </summary>
    private static void RegisterLeaveManagementJobs()
    {
        // Annual leave balance allocation on January 1st at 1:00 AM
        // Cron: "0 1 1 1 *" = At 01:00 on January 1st
        RecurringJob.AddOrUpdate<ILeaveBalanceResetJob>(
            recurringJobId: "annual-leave-balance-allocation",
            methodCall: job => job.AllocateLeaveBalancesForNewYearAsync(),
            cronExpression: "0 1 1 1 *",
            options: new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            });
    }
}
