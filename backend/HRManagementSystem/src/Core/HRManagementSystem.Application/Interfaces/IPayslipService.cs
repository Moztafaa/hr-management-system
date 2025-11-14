namespace HRManagementSystem.Application.Interfaces;

public interface IPayslipService
{
    Task<Response<PayslipDto>> GeneratePayslipAsync(int employeeId, int month, int year);
    Task<Response<IEnumerable<PayslipDto>>> GeneratePayslipsForMonthAsync(int month, int year);
    Task<Response<PayslipDto>> GetByIdAsync(int id);
    Task<Response<IEnumerable<PayslipDto>>> GetByEmployeeAsync(int employeeId);
    Task<Response<IEnumerable<PayslipDto>>> GetByMonthAsync(int month, int year);
    Task<Response<bool>> DeleteAsync(int id);
}
