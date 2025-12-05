using HRManagementSystem.Application.DTOs.Resignation;

namespace HRManagementSystem.Application.Interfaces.IResignationServices;

public interface IResignationService
{
    Task<Response<IEnumerable<ResignationDto>>> GetAllResignationsAsync(CancellationToken cancellationToken = default);
    Task<Response<ResignationDto>> GetResignationByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<ResignationDto>> CreateResignationAsync(CreateResignationDto dto, CancellationToken cancellationToken = default);
    Task<Response<bool>> WithdrawResignationAsync(WithdrawResignationDto dto, CancellationToken cancellationToken = default);
    Task<Response<bool>> DeleteResignationAsync(int id, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<ResignationDto>>> GetResignationsByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
    Task<Response<IEnumerable<ResignationDto>>> GetPendingResignationsForApproverAsync(int approverId, CancellationToken cancellationToken = default);
    Task<Response<ResignationDto?>> GetActiveResignationByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default);
}
