namespace HRManagementSystem.Application.Services;

public class PayslipService(
    IPayslipRepository _payslipRepository,
    IEmployeeRepository _employeeRepository,
    IEmployeeAllowanceRepository _employeeAllowanceRepository,
    IEmployeeDeductionRepository _employeeDeductionRepository,
    IMapper _mapper) : IPayslipService
{
    public async Task<Response<PayslipDto>> GeneratePayslipAsync(int employeeId, int month, int year)
    {
        try
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
                return new Response<PayslipDto>(null!, "Employee not found.", true);

            var allowances = await _employeeAllowanceRepository.GetByEmployeeIdAsync(employeeId);
            var deductions = await _employeeDeductionRepository.GetByEmployeeIdAsync(employeeId);

            decimal totalAllowances = allowances.Sum(a => a.Amount);
            decimal totalDeductions = deductions.Sum(d => d.Amount);
            decimal netSalary = employee.BasicSalary + totalAllowances - totalDeductions;

            var payslip = new Payslip
            {
                EmployeeId = employeeId,
                Month = month,
                Year = year,
                BasicSalary = employee.BasicSalary,
                TotalAllowances = totalAllowances,
                TotalDeductions = totalDeductions,
                NetSalary = netSalary,
                GeneratedAt = DateTime.UtcNow
            };

            await _payslipRepository.AddAsync(payslip);

            var payslipDto = _mapper.Map<PayslipDto>(payslip);
            payslipDto.Allowances = _mapper.Map<List<AllowanceDto>>(allowances.Select(ea => ea.Allowance));
            payslipDto.Deductions = _mapper.Map<List<DeductionDto>>(deductions.Select(ed => ed.Deduction));

            return new Response<PayslipDto>(payslipDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<PayslipDto>(null!, $"Error generating payslip: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<PayslipDto>>> GeneratePayslipsForMonthAsync(int month, int year)
    {
        try
        {
            var employees = await _employeeRepository.GetAllAsync();
            var payslipDtos = new List<PayslipDto>();

            foreach (var employee in employees)
            {
                var result = await GeneratePayslipAsync(employee.Id, month, year);
                if (!result.HasError && result.Data != null)
                    payslipDtos.Add(result.Data);
            }

            return new Response<IEnumerable<PayslipDto>>(payslipDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<PayslipDto>>(null!, $"Error generating payslips for month: {ex.Message}", true);
        }
    }

    public async Task<Response<PayslipDto>> GetByIdAsync(int id)
    {
        try
        {
            var payslip = await _payslipRepository.GetByIdAsync(id);
            if (payslip == null)
                return new Response<PayslipDto>(null!, "Payslip not found.", true);

            var dto = _mapper.Map<PayslipDto>(payslip);
            return new Response<PayslipDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<PayslipDto>(null!, $"Error retrieving payslip: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<PayslipDto>>> GetByEmployeeAsync(int employeeId)
    {
        try
        {
            var payslips = await _payslipRepository.GetByEmployeeIdAsync(employeeId);
            var dtoList = _mapper.Map<IEnumerable<PayslipDto>>(payslips);
            return new Response<IEnumerable<PayslipDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<PayslipDto>>(null!, $"Error retrieving payslips: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<PayslipDto>>> GetByMonthAsync(int month, int year)
    {
        try
        {
            var payslips = await _payslipRepository.GetByMonthAsync(month, year);
            var dtoList = _mapper.Map<IEnumerable<PayslipDto>>(payslips);
            return new Response<IEnumerable<PayslipDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<PayslipDto>>(null!, $"Error retrieving payslips for month: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        try
        {
            var payslip = await _payslipRepository.GetByIdAsync(id);
            if (payslip == null)
                return new Response<bool>(false, "Payslip not found.", true);

            await _payslipRepository.DeleteAsync(id);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error deleting payslip: {ex.Message}", true);
        }
    }
}
