namespace HRManagementSystem.Application.Interfaces;

/// <summary>
/// Service interface for payslip generation background jobs.
/// Handles batch payslip generation for all employees.
/// </summary>
public interface IPayslipGenerationJob
{
    /// <summary>
    /// Generates payslips for all active employees for a specific month.
    /// This is a long-running operation that should be executed as a background job.
    /// </summary>
    /// <param name="month">The month (1-12) for which to generate payslips.</param>
    /// <param name="year">The year for which to generate payslips.</param>
    Task GenerateAllPayslipsAsync(int month, int year);

    /// <summary>
    /// Generates a payslip for a single employee.
    /// Useful for queuing individual payslip generation as separate background jobs.
    /// </summary>
    /// <param name="employeeId">The employee ID.</param>
    /// <param name="month">The month (1-12) for which to generate the payslip.</param>
    /// <param name="year">The year for which to generate the payslip.</param>
    Task GeneratePayslipForEmployeeAsync(int employeeId, int month, int year);

    /// <summary>
    /// Sends payslip notification email and creates in-app notification for an employee.
    /// </summary>
    /// <param name="employeeId">The employee ID.</param>
    /// <param name="payslipId">The generated payslip ID.</param>
    /// <param name="month">The payslip month.</param>
    /// <param name="year">The payslip year.</param>
    /// <param name="netSalary">The net salary amount.</param>
    Task SendPayslipNotificationAsync(int employeeId, int payslipId, int month, int year, decimal netSalary);
}
