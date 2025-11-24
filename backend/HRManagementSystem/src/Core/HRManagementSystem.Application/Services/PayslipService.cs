namespace HRManagementSystem.Application.Services;

public class PayslipService(
    IUnitOfWork _unitOfWork,
    IMapper _mapper) : IPayslipService
{
    public async Task<Response<PayslipDto>> GeneratePayslipAsync(int employeeId, int month, int year, CancellationToken cancellationToken)
    {
        try
        {
            Employee? employee = await _unitOfWork.Employees.GetByIdAsync(employeeId, cancellationToken);
            if (employee == null)
            {
                return new Response<PayslipDto>(null!, "Employee not found.", true);
            }

            Payslip? existingPayslip = await _unitOfWork.Payslips.GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);
            if (existingPayslip != null)
            {
                await _unitOfWork.Payslips.DeleteAsync(existingPayslip.Id, cancellationToken);
            }

            var employeeAllowances = (await _unitOfWork.EmployeeAllowances
                .GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken))
                .Where(a => a.Month == month && a.Year == year)
                .ToList();

            IEnumerable<EmployeeDeduction> employeeDeductions = await _unitOfWork.EmployeeDeductions.GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);

            List<AllowanceDto> allowancesForDto = MapEmployeeAllowances(employeeAllowances, month, year);
            List<DeductionDto> deductionsForDto = MapEmployeeDeductions(employeeDeductions, month, year);

            decimal totalAllowances = allowancesForDto.Sum(a => a.Value);
            decimal totalDeductions = deductionsForDto.Sum(d => d.Value);

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
                GeneratedAt = DateTime.Now
            };

            await _unitOfWork.Payslips.AddAsync(payslip, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            PayslipDto payslipDto = _mapper.Map<PayslipDto>(payslip);
            payslipDto.Allowances = allowancesForDto;
            payslipDto.Deductions = deductionsForDto;

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
            IEnumerable<Employee> employees = await _unitOfWork.Employees.GetAllAsync(cancellationToken);
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
            IEnumerable<Payslip> payslips = await _unitOfWork.Payslips.GetByEmployeeIdAsync(employeeId, cancellationToken);
            var dtoList = new List<PayslipDto>();

            foreach (Payslip payslip in payslips)
            {
                PayslipDto dto = _mapper.Map<PayslipDto>(payslip);

                List<AllowanceDto> allowances = MapEmployeeAllowances(payslip.Employee.EmployeeAllowances, payslip.Month, payslip.Year);
                List<DeductionDto> deductions = MapEmployeeDeductions(payslip.Employee.EmployeeDeductions, payslip.Month, payslip.Year);

                dto.Allowances = allowances;
                dto.Deductions = deductions;

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
            Employee? employee = await _unitOfWork.Employees.GetByIdAsync(employeeId, cancellationToken);
            if (employee == null)
            {
                return new Response<PayslipDto>(null!, "Employee not found.", true);
            }

            Payslip? payslip = await _unitOfWork.Payslips.GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);
            if (payslip == null)
            {
                return new Response<PayslipDto>(null!, "Payslip not found for this employee and month.", true);
            }

            PayslipDto dto = _mapper.Map<PayslipDto>(payslip);

            List<AllowanceDto> allowances = MapEmployeeAllowances(payslip.Employee.EmployeeAllowances, payslip.Month, payslip.Year);
            List<DeductionDto> deductions = MapEmployeeDeductions(payslip.Employee.EmployeeDeductions, payslip.Month, payslip.Year);

            dto.Allowances = allowances;
            dto.Deductions = deductions;

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
            IEnumerable<Payslip> payslips = await _unitOfWork.Payslips.GetByMonthAsync(month, year, cancellationToken);
            var dtoList = new List<PayslipDto>();

            foreach (Payslip payslip in payslips)
            {
                PayslipDto dto = _mapper.Map<PayslipDto>(payslip);

                List<AllowanceDto> allowances = MapEmployeeAllowances(payslip.Employee.EmployeeAllowances, payslip.Month, payslip.Year);
                List<DeductionDto> deductions = MapEmployeeDeductions(payslip.Employee.EmployeeDeductions, payslip.Month, payslip.Year);

                dto.Allowances = allowances;
                dto.Deductions = deductions;

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
            Payslip? payslip = await _unitOfWork.Payslips.GetByIdAsync(id, cancellationToken);
            if (payslip == null)
            {
                return new Response<bool>(false, "Payslip not found.", true);
            }

            await _unitOfWork.Payslips.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error deleting payslip: {ex.Message}", true);
        }
    }

    private List<AllowanceDto> MapEmployeeAllowances(IEnumerable<EmployeeAllowance> employeeAllowances, int month, int year) =>
    employeeAllowances
        .Where(a => a.Month == month && a.Year == year)
        .Select(ea => new AllowanceDto
        {
            Id = ea.AllowanceId,
            Name = ea.Allowance?.Name ?? "No Allowance",
            Value = ea.Amount != 0 ? ea.Amount : ea.Allowance?.Value ?? 0,
            IsPercentage = ea.Allowance?.IsPercentage ?? false
        })
        .ToList();

    private List<DeductionDto> MapEmployeeDeductions(IEnumerable<EmployeeDeduction> employeeDeductions, int month, int year) =>
        employeeDeductions
            .Where(a => a.Month == month && a.Year == year)
            .Select(ed => new DeductionDto
            {
                Id = ed.DeductionId,
                Name = ed.Deduction?.Name ?? "No Deduction",
                Value = ed.Amount != 0 ? ed.Amount : ed.Deduction?.Value ?? 0,
                IsPercentage = ed.Deduction?.IsPercentage ?? false
            })
            .ToList();

}
