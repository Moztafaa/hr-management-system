namespace HRManagementSystem.Application.Interfaces.ILeaveServices;
public interface ILeaveTypeService
{
    Task<Response<IEnumerable<LeaveTypeDto>>> GetAllLeaveTypesAsync(CancellationToken cancellationToken = default);
    Task<Response<LeaveTypeDto>> GetLeaveTypeByIdAsync(int id, CancellationToken cancellationToken = default);
}
