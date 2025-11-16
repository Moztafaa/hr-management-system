using HRManagementSystem.Application.Interfaces;
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
    IPayslipRepository Payslips { get; }
    IAllowanceRepository Allowances { get; }
    IDeductionRepository Deductions { get; }
    IEmployeeAllowanceRepository EmployeeAllowances { get; }
    IEmployeeDeductionRepository EmployeeDeductions { get; }
    IEmployeeRepository Employees { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
