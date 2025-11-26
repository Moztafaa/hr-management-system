namespace HRManagementSystem.Application.Interfaces;

public interface IPayslipService
{
    Task<Response<PayslipDto>> GeneratePayslipAsync(int employeeId, int month, int year, IEnumerable<Deduction> fixedDeductions, CancellationToken cancellationToken);
    Task<Response<IEnumerable<PayslipDto>>> GeneratePayslipsForMonthAsync(int month, int year, IEnumerable<Deduction> fixedDeductions, CancellationToken cancellationToken);
    Task<Response<PayslipDto>> GetByIdAsync(int id, IEnumerable<Deduction> fixedDeductions, CancellationToken cancellationToken);
    Task<Response<IEnumerable<PayslipDto>>> GetByEmployeeAsync(int employeeId, IEnumerable<Deduction> fixedDeductions, CancellationToken cancellationToken);
    Task<Response<PayslipDto>> GetEmployeePayslipForMonthAsync(int employeeId, int month, int year, IEnumerable<Deduction> fixedDeductions, CancellationToken cancellationToken);
    Task<Response<IEnumerable<PayslipDto>>> GetByMonthAsync(int month, int year, IEnumerable<Deduction> fixedDeductions, CancellationToken cancellationToken);
    Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken);
}
