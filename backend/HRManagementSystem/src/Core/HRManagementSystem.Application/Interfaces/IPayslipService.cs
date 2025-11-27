using HRManagementSystem.Application.DTOs.File;

namespace HRManagementSystem.Application.Interfaces;

public interface IPayslipService
{
    Task<Response<PayslipDto>> GeneratePayslipAsync(int employeeId, int month, int year, CancellationToken cancellationToken);


    Task<Response<IEnumerable<PayslipDto>>> GeneratePayslipsForMonthAsync(int month, int year, CancellationToken cancellationToken);

    Task<Response<IEnumerable<PayslipDto>>> GetByEmployeeAsync(int employeeId, CancellationToken cancellationToken);


    Task<Response<PayslipDto>> GetEmployeePayslipForMonthAsync(int employeeId, int month, int year, CancellationToken cancellationToken);

    Task<Response<IEnumerable<PayslipDto>>> GetByMonthAsync(int month, int year, CancellationToken cancellationToken);


    Task<Response<PayslipDto>> GetByIdAsync(int id, CancellationToken cancellationToken);


    Task<Response<bool>> ExistsAsync(int employeeId, int month, int year, CancellationToken cancellationToken);

    Task<Response<FileExportDto>> ExportToPdfAsync(int id, CancellationToken cancellationToken);

    Task<Response<FileExportDto>> ExportMonthToExcelAsync(int month, int year, CancellationToken cancellationToken);

    Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken);
}
