using HRManagementSystem.Domain.Interfaces.LeaveRepository;
using HRManagementSystem.Infrastructure.Repositories.ILeaveRepository;
using Microsoft.EntityFrameworkCore.Storage;

namespace HRManagementSystem.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly Dictionary<Type, object> _repositories;
    private IDbContextTransaction? _transaction;

    private IDepartmentRepository? _departments;
    private IDesignationRepository? _designations;
    private ICandidateRepository? _candidates;
    private IJobPostingRepository? _jobPostings;
    private IJobApplicationRepository? _jobApplications;
    private ILeaveRequestRepository? _leaveRequests;
    private ILeaveApprovalRepository? _leaveApprovals;
    private IEmployeeLeaveBalanceRepository? _employeeLeaveBalances;
    private ILeaveTypeRepository? _leaveTypes;
    private IPayslipRepository? _payslips;
    private IAllowanceRepository? _allowances;
    private IDeductionRepository? _deductions;
    private IEmployeeAllowanceRepository? _employeeAllowances;
    private IEmployeeDeductionRepository? _employeeDeductions;
    private IEmployeeRepository? _employees;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IDepartmentRepository Departments => _departments ??= new DepartmentRepository(_context);

    public IDesignationRepository Designations => _designations ??= new DesignationRepository(_context);

    public ICandidateRepository Candidates => _candidates ??= new CandidateRepository(_context);

    public IJobPostingRepository JobPostings => _jobPostings ??= new JobPostingRepository(_context);

    public IJobApplicationRepository JobApplications => _jobApplications ??= new JobApplicationRepository(_context);

    public ILeaveRequestRepository LeaveRequests => _leaveRequests ??= new LeaveRequestRepository(_context);

    public ILeaveApprovalRepository LeaveApprovals => _leaveApprovals ??= new LeaveApprovalRepository(_context);

    public IEmployeeLeaveBalanceRepository EmployeeLeaveBalances => _employeeLeaveBalances ??= new EmployeeLeaveBalanceRepository(_context);

    public ILeaveTypeRepository LeaveTypes => _leaveTypes ??= new LeaveTypeRepository(_context);

    public IPayslipRepository Payslips => _payslips ??= new PayslipRepository(_context);

    public IAllowanceRepository Allowances => _allowances ??= new AllowanceRepository(_context);

    public IDeductionRepository Deductions => _deductions ??= new DeductionRepository(_context);

    public IEmployeeAllowanceRepository EmployeeAllowances => _employeeAllowances ??= new EmployeeAllowanceRepository(_context);

    public IEmployeeDeductionRepository EmployeeDeductions => _employeeDeductions ??= new EmployeeDeductionRepository(_context);

    public IEmployeeRepository Employees => _employees ??= new EmployeeRepository(_context);

    public IRepository<T> Repository<T>() where T : class
    {
        Type type = typeof(T);

        if (!_repositories.ContainsKey(type))
        {
            _repositories[type] = new Repository<T>(_context);
        }

        return (IRepository<T>)_repositories[type];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            if (_transaction != null)
            {
                await _transaction.CommitAsync(cancellationToken);
            }
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
