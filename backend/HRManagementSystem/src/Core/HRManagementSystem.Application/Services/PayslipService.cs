using System.Collections.Generic;

namespace HRManagementSystem.Application.Services;

public class PayslipService(IUnitOfWork _unitOfWork, IMapper _mapper) : IPayslipService
{
    // Generate single payslip 
    public async Task<Response<PayslipDto>> GeneratePayslipAsync(int employeeId, int month, int year, CancellationToken cancellationToken)
    {
        try
        {
            Employee? employee = await _unitOfWork.Employees.GetByIdAsync(employeeId, cancellationToken);
            if (employee == null)
                return new Response<PayslipDto>(null!, "Employee not found.", true);

            // Allow regeneration
            Payslip? existingPayslip = await _unitOfWork.Payslips.GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);
            if (existingPayslip != null)
                await _unitOfWork.Payslips.DeleteAsync(existingPayslip.Id, cancellationToken);

            IEnumerable<EmployeeAllowance> employeeAllowances = await _unitOfWork.EmployeeAllowances
                .GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);

            IEnumerable<EmployeeDeduction> employeeDeductions = await _unitOfWork.EmployeeDeductions
                .GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);

            IEnumerable<Deduction> fixedDeductions = await _unitOfWork.Deductions.GetFixedDeductionsAsync(cancellationToken);

            PayslipDto payslipDto = MapPayslip(employee, employeeAllowances, employeeDeductions, fixedDeductions, month, year);

            var payslip = new Payslip
            {
                EmployeeId = employeeId,
                Month = month,
                Year = year,
                BasicSalary = employee.BasicSalary,
                TotalAllowances = payslipDto.TotalAllowances,
                TotalDeductions = payslipDto.TotalDeductions,
                NetSalary = payslipDto.NetSalary,
                GeneratedAt = DateTime.UtcNow
            };

            await _unitOfWork.Payslips.AddAsync(payslip, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            payslipDto.Id = payslip.Id; // Map saved ID

            return new Response<PayslipDto>(payslipDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<PayslipDto>(null!, $"Error generating payslip: {ex.Message}", true);
        }
    }

    // Generate payslips for a month (parallel) 
    public async Task<Response<IEnumerable<PayslipDto>>> GeneratePayslipsForMonthAsync(int month, int year, CancellationToken cancellationToken)
    {
        IEnumerable<Employee> employees = await _unitOfWork.Employees.GetAllAsync(cancellationToken);
        var payslipDtos = new List<PayslipDto>();

        foreach (Employee employee in employees)
        {
            Response<PayslipDto> result = await GeneratePayslipAsync(employee.Id, month, year, cancellationToken);
            if (!result.HasError && result.Data != null)
                payslipDtos.Add(result.Data);
        }

        return new Response<IEnumerable<PayslipDto>>(payslipDtos, string.Empty, false);
    }

    //  Get single payslip 
    public async Task<Response<PayslipDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            Payslip? payslip = await _unitOfWork.Payslips.GetByIdAsync(id, cancellationToken);
            if (payslip == null)
                return new Response<PayslipDto>(null!, "Payslip not found.", true);

            IEnumerable<EmployeeAllowance> employeeAllowances = await _unitOfWork.EmployeeAllowances
                .GetByEmployeeAndMonthAsync(payslip.EmployeeId, payslip.Month, payslip.Year, cancellationToken);

            IEnumerable<EmployeeDeduction> employeeDeductions = await _unitOfWork.EmployeeDeductions
                .GetByEmployeeAndMonthAsync(payslip.EmployeeId, payslip.Month, payslip.Year, cancellationToken);

            IEnumerable<Deduction> fixedDeductions = await _unitOfWork.Deductions.GetFixedDeductionsAsync(cancellationToken);

            PayslipDto payslipDto = MapPayslip(payslip.Employee, employeeAllowances, employeeDeductions, fixedDeductions, payslip.Month, payslip.Year);
            payslipDto.Id = payslip.Id;

            return new Response<PayslipDto>(payslipDto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<PayslipDto>(null!, $"Error retrieving payslip: {ex.Message}", true);
        }
    }

    //  Get payslips by employee
    public async Task<Response<IEnumerable<PayslipDto>>> GetByEmployeeAsync(int employeeId, CancellationToken cancellationToken)
    {
        try
        {
            Employee? employee = await _unitOfWork.Employees.GetByIdAsync(employeeId, cancellationToken);
            if (employee == null)
                return new Response<IEnumerable<PayslipDto>>(null!, "Employee not found.", true);

            IEnumerable<Payslip> payslips = await _unitOfWork.Payslips.GetByEmployeeIdAsync(employeeId, cancellationToken);
            IEnumerable<Deduction> fixedDeductions = await _unitOfWork.Deductions.GetFixedDeductionsAsync(cancellationToken);

            var dtoList = new List<PayslipDto>();
            foreach (Payslip payslip in payslips)
            {
                IEnumerable<EmployeeAllowance> allowances = await _unitOfWork.EmployeeAllowances.GetByEmployeeAndMonthAsync(employeeId, payslip.Month, payslip.Year, cancellationToken);
                IEnumerable<EmployeeDeduction> deductions = await _unitOfWork.EmployeeDeductions.GetByEmployeeAndMonthAsync(employeeId, payslip.Month, payslip.Year, cancellationToken);

                PayslipDto dto = MapPayslip(employee, allowances, deductions, fixedDeductions, payslip.Month, payslip.Year);
                dto.Id = payslip.Id;

                dtoList.Add(dto);
            }

            return new Response<IEnumerable<PayslipDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<PayslipDto>>(null!, $"Error retrieving payslips: {ex.Message}", true);
        }
    }

    //  Get payslips by month 
    public async Task<Response<IEnumerable<PayslipDto>>> GetByMonthAsync(int month, int year, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Payslip> payslips = await _unitOfWork.Payslips.GetByMonthAsync(month, year, cancellationToken);
            IEnumerable<Deduction> fixedDeductions = await _unitOfWork.Deductions.GetFixedDeductionsAsync(cancellationToken);

            var dtoList = new List<PayslipDto>();
            foreach (Payslip payslip in payslips)
            {
                Employee? employee = await _unitOfWork.Employees.GetByIdAsync(payslip.EmployeeId, cancellationToken);
                IEnumerable<EmployeeAllowance> allowances = await _unitOfWork.EmployeeAllowances.GetByEmployeeAndMonthAsync(payslip.EmployeeId, month, year, cancellationToken);
                IEnumerable<EmployeeDeduction> deductions = await _unitOfWork.EmployeeDeductions.GetByEmployeeAndMonthAsync(payslip.EmployeeId, month, year, cancellationToken);

                PayslipDto dto = MapPayslip(employee, allowances, deductions, fixedDeductions, month, year);
                dto.Id = payslip.Id;

                dtoList.Add(dto);
            }

            return new Response<IEnumerable<PayslipDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<PayslipDto>>(null!, $"Error retrieving payslips for month: {ex.Message}", true);
        }
    }

    //  Get employee payslip for specific month
    public async Task<Response<PayslipDto>> GetEmployeePayslipForMonthAsync(int employeeId,int month,int year,CancellationToken cancellationToken)
    {
        Employee? employee = await _unitOfWork.Employees.GetByIdAsync(employeeId, cancellationToken);
        if (employee == null)
            return new Response<PayslipDto>(null!, "Employee not found.", true);

        IEnumerable<EmployeeAllowance> allowances = await _unitOfWork.EmployeeAllowances.GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);
        IEnumerable<EmployeeDeduction> deductions = await _unitOfWork.EmployeeDeductions.GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);

        IEnumerable<Deduction> fixedDeductions = await _unitOfWork.Deductions.GetFixedDeductionsAsync(cancellationToken);

        PayslipDto payslipDto = MapPayslip(
            employee,
            allowances,
            deductions,
            fixedDeductions,
            month,
            year
        );

        return new Response<PayslipDto>(payslipDto, string.Empty, false);
    }

    // Delete payslip 
    public async Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            Payslip? payslip = await _unitOfWork.Payslips.GetByIdAsync(id, cancellationToken);
            if (payslip == null)
                return new Response<bool>(false, "Payslip not found.", true);

            await _unitOfWork.Payslips.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Response<bool>(true, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error deleting payslip: {ex.Message}", true);
        }
    }

    // --- Mapping helper ---
    private PayslipDto MapPayslip(Employee employee, IEnumerable<EmployeeAllowance> allowances, IEnumerable<EmployeeDeduction> deductions,
                                  IEnumerable<Deduction> fixedDeductions, int month, int year)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee));

        var allowanceDtos = allowances
            .Where(a => a.Month == month && a.Year == year)
            .Select(a =>
            {
                bool isPercentage = a.Allowance?.IsPercentage ?? false;
                decimal value = isPercentage ? employee.BasicSalary * (a.Amount ?? a.Allowance!.Value) / 100
                                         : (a.Amount ?? a.Allowance!.Value);
                return new AllowanceDto
                {
                    Id = a.Allowance?.Id ?? 0,
                    Name = a.Allowance?.Name ?? "No Allowance",
                    Value = value,
                    IsPercentage = isPercentage
                };
            })
            .ToList();

        var monthlyEmployeeDeductions = deductions.Where(d => d.Month == month && d.Year == year).ToList();
        var employeeDeductionIds = monthlyEmployeeDeductions.Where(d => d.Deduction != null).Select(d => d.DeductionId).ToHashSet();

        var deductionDtos = monthlyEmployeeDeductions
            .Where(d => d.Deduction != null)
            .Select(d =>
            {
                Deduction deduction = d.Deduction!;
                bool isPercentage = deduction.IsPercentage;
                decimal value = isPercentage ? employee.BasicSalary * (d.Amount ?? deduction.Value) / 100 : (d.Amount ?? deduction.Value);
                return new DeductionDto
                {
                    Id = deduction.Id,
                    Name = deduction.Name,
                    Value = value,
                    IsPercentage = isPercentage,
                    IsFixed = deduction.IsFixed
                };
            })
            .ToList();

        // Add fixed deductions if not already applied
        deductionDtos.AddRange(fixedDeductions
            .Where(fd => !employeeDeductionIds.Contains(fd.Id))
            .Select(fd =>
            {
                decimal value = employee.BasicSalary * fd.Value / 100;
                return new DeductionDto
                {
                    Id = fd.Id,
                    Name = fd.Name,
                    Value = value,
                    IsPercentage = true,
                    IsFixed = fd.IsFixed
                };
            }));

        decimal totalAllowances = allowanceDtos.Sum(a => a.Value);
        decimal totalDeductions = deductionDtos.Sum(d => d.Value);
        decimal netSalary = employee.BasicSalary + totalAllowances - totalDeductions;

        return new PayslipDto
        {
            EmployeeId = employee.Id,
            EmployeeName = employee.FirstName + " " + employee.LastName,
            Month = month,
            Year = year,
            BasicSalary = employee.BasicSalary,
            Allowances = allowanceDtos,
            Deductions = deductionDtos,
            TotalAllowances = totalAllowances,
            TotalDeductions = totalDeductions,
            NetSalary = netSalary
        };
    }
}
