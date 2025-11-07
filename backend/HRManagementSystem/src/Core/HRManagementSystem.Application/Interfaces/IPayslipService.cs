namespace HRManagementSystem.Application.Interfaces;
public interface IPayslipService
{
    Task GeneratePayslipAsync(int employeeId, int month, int year);
    Task GeneratePayslipsForMonthAsync(int month, int year);
}
