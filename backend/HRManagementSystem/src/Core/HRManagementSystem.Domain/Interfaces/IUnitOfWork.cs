using HRManagementSystem.Domain.Interfaces.LeaveRepository;

namespace HRManagementSystem.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : class;
    IDepartmentRepository Departments { get; }
    IDesignationRepository Designations { get; }
    ICandidateRepository Candidates { get; }
    IJobPostingRepository JobPostings { get; }
    IJobApplicationRepository JobApplications { get; }
    ILeaveRequestRepository LeaveRequests { get; }
    ILeaveApprovalRepository LeaveApprovals { get; }
    IEmployeeLeaveBalanceRepository EmployeeLeaveBalances { get; }
    ILeaveTypeRepository LeaveTypes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
