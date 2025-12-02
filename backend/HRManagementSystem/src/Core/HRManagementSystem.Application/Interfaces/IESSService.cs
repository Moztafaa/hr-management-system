using HRManagementSystem.Application.Common;
using HRManagementSystem.Application.DTOs.ESS;
using HRManagementSystem.Application.DTOs.Leaves.LeaveBalanceDtos;
using HRManagementSystem.Application.DTOs.Leaves.LeaveRequestDtos;
using HRManagementSystem.Application.DTOs.Payroll.Payslip;
using HRManagementSystem.Application.DTOs.Training;

namespace HRManagementSystem.Application.Interfaces;

public interface IESSService
{
    Task<Response<ESSProfileDto>> GetProfileAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<Response<bool>> UpdateProfileAsync(int employeeId, UpdateESSProfileDto dto, CancellationToken cancellationToken = default);
    Task<Response<List<LeaveRequestDto>>> GetLeaveHistoryAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<Response<int>> SubmitLeaveRequestAsync(int employeeId, CreateLeaveRequestDto dto, CancellationToken cancellationToken = default);
    Task<Response<List<LeaveBalanceDto>>> GetLeaveBalancesAsync(int employeeId, int year, CancellationToken cancellationToken = default);
    Task<Response<List<PayslipDto>>> GetPayslipsAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<Response<PayslipDto>> GetPayslipByIdAsync(int employeeId, int payslipId, CancellationToken cancellationToken = default);
    Task<Response<ESSDashboardDto>> GetDashboardAsync(int employeeId, CancellationToken cancellationToken = default);

    // Training methods
    Task<Response<List<EmployeeTrainingDto>>> GetMyCoursesAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<Response<List<TrainingRequestDto>>> GetMyTrainingRequestsAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<Response<List<TrainingCourseDto>>> GetAvailableCoursesAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<Response<TrainingRequestDto>> SubmitTrainingRequestAsync(int employeeId, ESSTrainingRequestCreateDto dto, CancellationToken cancellationToken = default);
    Task<Response<bool>> CancelTrainingRequestAsync(int employeeId, int requestId, CancellationToken cancellationToken = default);
}
