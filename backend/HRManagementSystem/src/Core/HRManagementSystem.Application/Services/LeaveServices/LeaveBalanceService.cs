

namespace HRManagementSystem.Application.Services.LeaveServices;
public class LeaveBalanceService(
    IEmployeeLeaveBalanceRepository _leaveBalanceRepository,
    ILeaveTypeRepository _leaveTypeRepository,
    IEmployeeService _employeeService,
    IMapper _mapper) : ILeaveBalanceService
{

    // ✅ Get leave balances for one employee
    public async Task<Response<IEnumerable<LeaveBalanceDto>>> GetByEmployeeIdAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        IEnumerable<EmployeeLeaveBalance> balances = await _leaveBalanceRepository.GetByEmployeeIdAsync(employeeId, cancellationToken);
        IEnumerable<LeaveBalanceDto>? dtos = _mapper.Map<IEnumerable<LeaveBalanceDto>>(balances);
        return new Response<IEnumerable<LeaveBalanceDto>>(dtos, null, false);
    }
    // ✅ Get leave balances for one employee With year
    public async Task<Response<IEnumerable<LeaveBalanceDto>>> GetByEmployeeIdAndYearAsync(int employeeId, int year, CancellationToken cancellationToken = default)
    {
        IEnumerable<EmployeeLeaveBalance> balances = await _leaveBalanceRepository.GetByEmployeeIdAndYearAsync(employeeId, year, cancellationToken);
        IEnumerable<LeaveBalanceDto>? dtos = _mapper.Map<IEnumerable<LeaveBalanceDto>>(balances);
        return new Response<IEnumerable<LeaveBalanceDto>>(dtos, null, false);
    }

    // ✅ Allocate balances for all leave types to a specific employee (used at hire or new year)
    public async Task<Response<bool>> AllocateInitialBalancesAsync(int employeeId, CancellationToken cancellationToken = default)
    {
        try
        {
            Response<EmployeeDto> employee = await _employeeService.GetEmployeeByIdAsync(employeeId, cancellationToken);
            if (employee.HasError == true)
                return new Response<bool>(false, "Employee not found", true);

            int yearsOfService = (int)((DateTime.UtcNow - employee.Data.HireDate).TotalDays / 365);

            IEnumerable<LeaveType> leaveTypes = await _leaveTypeRepository.GetAllAsync(cancellationToken);

            foreach (LeaveType type in leaveTypes)
            {
                EmployeeLeaveBalance? existing = await _leaveBalanceRepository.GetByEmployeeAndTypeAndYearAsync(employeeId, type.Id, DateTime.UtcNow.Year, cancellationToken);
                if (existing != null)
                    continue;
                if (type.GenderRestriction != null && !string.Equals(type.GenderRestriction, employee.Data.Gender, StringComparison.OrdinalIgnoreCase))
                    continue;

                int totalDays = type.MaxDays;

                // 🔹 Adjust annual leave based on years of service
                if (type.Name.Equals("Annual Leave", StringComparison.OrdinalIgnoreCase))
                {
                    if (yearsOfService < 5)
                        totalDays = 21;
                    else if (yearsOfService < 10)
                        totalDays = 25;
                    else
                        totalDays = 30;
                }

                var balance = new EmployeeLeaveBalance
                {
                    EmployeeId = employeeId,
                    LeaveTypeId = type.Id,
                    TotalAllocated = totalDays,
                    UsedDays = 0,
                    RemainingDays=totalDays,
                    Year = DateTime.UtcNow.Year
                };

                await _leaveBalanceRepository.AddAsync(balance, cancellationToken);
            }

            return new Response<bool>(true, "Leave balances allocated successfully.", false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error while allocating balances: {ex.Message}", true);
        }
    }

    // ✅ Deduct leave days after an approved request
    public async Task<Response<bool>> DeductLeaveDaysAsync(int employeeId, int leaveTypeId, int leaveDays, CancellationToken cancellationToken = default)
    {
        EmployeeLeaveBalance? balance = await _leaveBalanceRepository.GetByEmployeeAndTypeAndYearAsync(employeeId, leaveTypeId, DateTime.UtcNow.Year, cancellationToken);
        if (balance == null)
            return new Response<bool>(false, "Leave balance not found for this employee.", true);

        if (balance.TotalAllocated - balance.UsedDays < leaveDays)
            return new Response<bool>(false, "Insufficient leave balance.", true);

        balance.UsedDays += leaveDays;
        balance.RemainingDays-= leaveDays;
        await _leaveBalanceRepository.UpdateAsync(balance, cancellationToken);
        return new Response<bool>(true, "Leave days deducted successfully.", false);
    }

    // ✅ Allocate balances for all employees (e.g., at the start of each year)
    public async Task<Response<bool>> AllocateBalancesForNewYearAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            Response<IEnumerable<EmployeeDto>> response = await _employeeService.GetAllEmployeesAsync(cancellationToken);

            if (response.HasError || response.Data == null)
                return new Response<bool>(false, "No employees found to allocate balances.", true);

            foreach (EmployeeDto emp in response.Data)
            {
                await AllocateInitialBalancesAsync(emp.Id, cancellationToken);
            }

            return new Response<bool>(true, "Leave balances successfully allocated for all employees.", false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error while allocating balances for all employees: {ex.Message}", true);
        }
    }
}
