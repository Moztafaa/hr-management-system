namespace HRManagementSystem.Application.Services;

public class PayslipService(
    IPayslipRepository _payslipRepository,
    IEmployeeRepository _employeeRepository,
    IEmployeeAllowanceRepository _employeeAllowanceRepository,
    IEmployeeDeductionRepository _employeeDeductionRepository,
    IMapper _mapper) : IPayslipService
{
    public async Task<Response<PayslipDto>> GeneratePayslipAsync(int employeeId, int month, int year, CancellationToken cancellationToken)
    {
        try
        {
            Employee? employee = await _employeeRepository.GetByIdAsync(employeeId);
            if (employee == null)
            {
                return new Response<PayslipDto>(null!, "Employee not found.", true);
            }

            Payslip? existingPayslip = await _payslipRepository.GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);
            if (existingPayslip != null)
            {
                return new Response<PayslipDto>(_mapper.Map<PayslipDto>(existingPayslip), "Payslip already exists for this month.", false);
            }

            IEnumerable<EmployeeAllowance> employeeAllowances = await _employeeAllowanceRepository.GetByEmployeeIdAsync(employeeId, cancellationToken);
            IEnumerable<EmployeeDeduction> employeeDeductions = await _employeeDeductionRepository.GetByEmployeeIdAsync(employeeId, cancellationToken);

            var allowancesForDto = employeeAllowances.Select(ea => new AllowanceDto
            {
                Id = ea.AllowanceId,
                Name = ea.Allowance.Name,
                Amount = ea.Amount != 0 ? ea.Amount : ea.Allowance.Amount
            }).ToList();

            decimal totalAllowances = allowancesForDto.Sum(a => a.Amount);

            var deductionsForDto = employeeDeductions.Select(ed => new DeductionDto
            {
                Id = ed.DeductionId,
                Name = ed.Deduction.Name,
                Amount = ed.Amount != 0 ? ed.Amount : ed.Deduction.Amount
            }).ToList();

            decimal totalDeductions = deductionsForDto.Sum(d => d.Amount);

            decimal netSalary = employee.BasicSalary + totalAllowances - totalDeductions;

            // Create payslip
            var payslip = new Payslip
            {
                EmployeeId = employeeId,
                Month = month,
                Year = year,
                BasicSalary = employee.BasicSalary,
                TotalAllowances = totalAllowances,
                TotalDeductions = totalDeductions,
                NetSalary = netSalary,
                GeneratedAt = DateTime.Now
            };

            await _payslipRepository.AddAsync(payslip, cancellationToken);

            // Map to DTO
            PayslipDto payslipDto = _mapper.Map<PayslipDto>(payslip);
            payslipDto.Allowances = _mapper.Map<List<AllowanceDto>>(allowancesForDto);
            payslipDto.Deductions = _mapper.Map<List<DeductionDto>>(deductionsForDto);

            return new Response<PayslipDto>(payslipDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<PayslipDto>(null!, $"Error generating payslip: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<PayslipDto>>> GeneratePayslipsForMonthAsync(int month, int year, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetAllAsync(cancellationToken);
            var payslipDtos = new List<PayslipDto>();

            foreach (Employee employee in employees)
            {
                Response<PayslipDto> result = await GeneratePayslipAsync(employee.Id, month, year, cancellationToken);
                if (!result.HasError && result.Data != null)
                {
                    payslipDtos.Add(result.Data);
                }
            }

            return new Response<IEnumerable<PayslipDto>>(payslipDtos, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<PayslipDto>>(null!, $"Error generating payslips for month: {ex.Message}", true);
        }
    }

    //public async Task<Response<PayslipDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        Payslip? payslip = await _payslipRepository.GetByIdAsync(id, cancellationToken);
    //        if (payslip == null)
    //        {
    //            return new Response<PayslipDto>(null!, "Payslip not found.", true);
    //        }

    //        PayslipDto dto = _mapper.Map<PayslipDto>(payslip);
    //        dto.Allowances = _mapper.Map<List<AllowanceDto>>(payslip.Employee.EmployeeAllowances.Select(ea => ea.Allowance));
    //        dto.Deductions = _mapper.Map<List<DeductionDto>>(payslip.Employee.EmployeeDeductions.Select(ed => ed.Deduction));

    //        return new Response<PayslipDto>(dto, string.Empty, false);
    //    }
    //    catch (Exception ex)
    //    {
    //        return new Response<PayslipDto>(null!, $"Error retrieving payslip: {ex.Message}", true);
    //    }
    //}

    public async Task<Response<IEnumerable<PayslipDto>>> GetByEmployeeAsync(int employeeId, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Payslip> payslips = await _payslipRepository.GetByEmployeeIdAsync(employeeId, cancellationToken);
            var dtoList = new List<PayslipDto>();

            foreach (Payslip payslip in payslips)
            {
                PayslipDto dto = _mapper.Map<PayslipDto>(payslip);
                dto.Allowances = _mapper.Map<List<AllowanceDto>>(payslip.Employee.EmployeeAllowances.Select(ea => ea.Allowance));
                dto.Deductions = _mapper.Map<List<DeductionDto>>(payslip.Employee.EmployeeDeductions.Select(ed => ed.Deduction));
                dtoList.Add(dto);
            }

            return new Response<IEnumerable<PayslipDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<PayslipDto>>(null!, $"Error retrieving payslips: {ex.Message}", true);
        }
    }

    public async Task<Response<PayslipDto>> GetEmployeePayslipForMonthAsync(int employeeId, int month, int year, CancellationToken cancellationToken)
    {
        try
        {
            Employee? employee = await _employeeRepository.GetByIdAsync(employeeId, cancellationToken);
            if (employee == null)
            {
                return new Response<PayslipDto>(null!, "Employee not found.", true);
            }

            Payslip? payslip = await _payslipRepository.GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);
            if (payslip == null)
            {
                return new Response<PayslipDto>(null!, "Payslip not found for this employee and month.", true);
            }

            PayslipDto dto = _mapper.Map<PayslipDto>(payslip);
            dto.Allowances = _mapper.Map<List<AllowanceDto>>(payslip.Employee.EmployeeAllowances.Select(ea => ea.Allowance));
            dto.Deductions = _mapper.Map<List<DeductionDto>>(payslip.Employee.EmployeeDeductions.Select(ed => ed.Deduction));

            return new Response<PayslipDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<PayslipDto>(null!, $"Error retrieving employee payslip for month: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<PayslipDto>>> GetByMonthAsync(int month, int year, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Payslip> payslips = await _payslipRepository.GetByMonthAsync(month, year, cancellationToken);
            var dtoList = new List<PayslipDto>();

            foreach (Payslip payslip in payslips)
            {
                PayslipDto dto = _mapper.Map<PayslipDto>(payslip);
                dto.Allowances = _mapper.Map<List<AllowanceDto>>(payslip.Employee.EmployeeAllowances.Select(ea => ea.Allowance));
                dto.Deductions = _mapper.Map<List<DeductionDto>>(payslip.Employee.EmployeeDeductions.Select(ed => ed.Deduction));
                dtoList.Add(dto);
            }

            return new Response<IEnumerable<PayslipDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<PayslipDto>>(null!, $"Error retrieving payslips for month: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            Payslip? payslip = await _payslipRepository.GetByIdAsync(id, cancellationToken);
            if (payslip == null)
            {
                return new Response<bool>(false, "Payslip not found.", true);
            }

            await _payslipRepository.DeleteAsync(id, cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error deleting payslip: {ex.Message}", true);
        }
    }
}
