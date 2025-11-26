using System.Collections.Generic;

namespace HRManagementSystem.Application.Services;

public class PayslipService( IUnitOfWork _unitOfWork,IMapper _mapper) : IPayslipService
{
    public async Task<Response<PayslipDto>> GeneratePayslipAsync(int employeeId, int month, int year, IEnumerable<Deduction> fixedDeductions, CancellationToken cancellationToken)
    {
        try
        {
            Employee? employee = await _unitOfWork.Employees.GetByIdAsync(employeeId, cancellationToken);
            if (employee == null)
                return new Response<PayslipDto>(null!, "Employee not found.", true);

            // To allow regeneration if there is any edits in the allowances/deductions
            Payslip? existingPayslip = await _unitOfWork.Payslips.GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);
            if (existingPayslip != null)
            {
                await _unitOfWork.Payslips.DeleteAsync(existingPayslip.Id, cancellationToken);
            }

            IEnumerable<EmployeeAllowance> employeeAllowances = await _unitOfWork.EmployeeAllowances
                .GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);

            IEnumerable<EmployeeDeduction> employeeDeductions = await _unitOfWork.EmployeeDeductions
                .GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);

            List<AllowanceDto> allowancesForDto = MapEmployeeAllowances(employeeAllowances, employee.BasicSalary, month, year);
            List<DeductionDto> deductionsForDto = MapEmployeeDeductions(employeeDeductions, employee.BasicSalary, month, year,fixedDeductions);

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

    public async Task<Response<PayslipDto>> GetByIdAsync(int id, IEnumerable<Deduction> fixedDeductions, CancellationToken cancellationToken)
    {
        try
        {
            Payslip? payslip = await _unitOfWork.Payslips.GetByIdAsync(id, cancellationToken);
            if (payslip == null)
                return new Response<PayslipDto>(null!, "Payslip not found.", true);

            IEnumerable<EmployeeAllowance> allowances =
                await _unitOfWork.EmployeeAllowances.GetByEmployeeAndMonthAsync(
                    payslip.EmployeeId, payslip.Month, payslip.Year, cancellationToken);

            IEnumerable<EmployeeDeduction> deductions =
                await _unitOfWork.EmployeeDeductions.GetByEmployeeAndMonthAsync(
                    payslip.EmployeeId, payslip.Month, payslip.Year, cancellationToken);

            PayslipDto dto = _mapper.Map<PayslipDto>(payslip);
            dto.Allowances = MapEmployeeAllowances(allowances, payslip.BasicSalary, payslip.Month, payslip.Year);
            dto.Deductions = MapEmployeeDeductions(deductions, payslip.BasicSalary, payslip.Month, payslip.Year, fixedDeductions);

            dto.TotalAllowances = dto.Allowances.Sum(a => a.Value);
            dto.TotalDeductions = dto.Deductions.Sum(d => d.Value);
            dto.NetSalary = payslip.BasicSalary + dto.TotalAllowances - dto.TotalDeductions;

            return new Response<PayslipDto>(dto, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<PayslipDto>(null!, $"Error retrieving payslip: {ex.Message}", true);
        }
    }

    // Mapping Methods with Percentage Logic
    private List<AllowanceDto> MapEmployeeAllowances(IEnumerable<EmployeeAllowance> employeeAllowances,decimal baseSalary,int month,int year)
    {
        return employeeAllowances
            .Where(a => a.Month == month && a.Year == year)
            .Select(ea =>
            {
                bool isPercentage = ea.Allowance?.IsPercentage ?? false;

                decimal finalValue;
                if (isPercentage)
                {
                    decimal percentage = ea.Amount > 0 ? ea.Amount : ea.Allowance?.Value ?? 0;
                    finalValue = baseSalary * percentage / 100;
                }
                else
                {
                    finalValue = ea.Amount > 0 ? ea.Amount : ea.Allowance?.Value ?? 0;
                }

                return new AllowanceDto
                {
                    Id = ea.AllowanceId,
                    Name = ea.Allowance?.Name ?? "No Allowance",
                    Value = finalValue,
                    IsPercentage = isPercentage
                };
            })
            .ToList();
    }
    private List<DeductionDto> MapEmployeeDeductions(IEnumerable<EmployeeDeduction> employeeDeductions,decimal baseSalary
                                                     ,int month,int year,IEnumerable<Deduction> fixedDeductions)
    {
        var result = new List<DeductionDto>();

        // Employee-specific deductions 
        IEnumerable<EmployeeDeduction> monthlyEmployeeDeductions = employeeDeductions.Where(d => d.Month == month && d.Year == year);

        foreach (EmployeeDeduction ed in monthlyEmployeeDeductions)
        {
            bool isPercentage = ed.Deduction?.IsPercentage ?? false;
            decimal finalValue;

            if (isPercentage)
            {
                decimal percentage = ed.Amount > 0 ? ed.Amount : (ed.Deduction?.Value ?? 0);
                finalValue = baseSalary * percentage / 100;
            }
            else
            {
                finalValue = ed.Amount > 0 ? ed.Amount : (ed.Deduction?.Value ?? 0);
            }

            result.Add(new DeductionDto
            {
                Id = ed.DeductionId,
                Name = ed.Deduction?.Name ?? "No Deduction",
                Value = finalValue,
                IsPercentage = isPercentage
            });
        }

        // Fixed deductions: ALWAYS percentage (social insurance, income tax)
        foreach (Deduction fd in fixedDeductions)
        {
            decimal percentage = fd.Value;   
            decimal finalValue = baseSalary * percentage / 100;

            result.Add(new DeductionDto
            {
                Id = fd.Id,
                Name = fd.Name,
                Value = finalValue,
                IsPercentage = true
            });
        }

        return result;
    }

    public async Task<Response<IEnumerable<PayslipDto>>> GeneratePayslipsForMonthAsync(int month, int year, IEnumerable<Deduction> fixedDeductions, CancellationToken cancellationToken)
    {
        IEnumerable<Employee> employees = await _unitOfWork.Employees.GetAllAsync(cancellationToken);
        var payslipDtos = new List<PayslipDto>();

        foreach (Employee employee in employees)
        {
            Response<PayslipDto> result = await GeneratePayslipAsync(employee.Id, month, year, fixedDeductions, cancellationToken);
            if (!result.HasError && result.Data != null)
                payslipDtos.Add(result.Data);
        }

        return new Response<IEnumerable<PayslipDto>>(payslipDtos, string.Empty, false);
    }

    public async Task<Response<PayslipDto>> GetEmployeePayslipForMonthAsync(int employeeId, int month, int year, IEnumerable<Deduction> fixedDeductions, CancellationToken cancellationToken)
    {
        Payslip? payslip = await _unitOfWork.Payslips.GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken);
        if (payslip == null)
            return new Response<PayslipDto>(null!, "Payslip not found.", true);

        List<AllowanceDto> allowances = MapEmployeeAllowances(await _unitOfWork.EmployeeAllowances
            .GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken), payslip.BasicSalary,month,year);

        List<DeductionDto> deductions = MapEmployeeDeductions(await _unitOfWork.EmployeeDeductions
            .GetByEmployeeAndMonthAsync(employeeId, month, year, cancellationToken), payslip.BasicSalary,month, year, fixedDeductions);

        PayslipDto dto = _mapper.Map<PayslipDto>(payslip);
        dto.Allowances = allowances;
        dto.Deductions = deductions;

        return new Response<PayslipDto>(dto, string.Empty, false);
    }


    public async Task<Response<IEnumerable<PayslipDto>>> GetByEmployeeAsync(int employeeId, IEnumerable<Deduction> fixedDeductions, CancellationToken cancellationToken)
    {
        try
        {
            Employee? employee = await _unitOfWork.Employees.GetByIdAsync(employeeId, cancellationToken);
            if (employee == null)
                return new Response<IEnumerable<PayslipDto>>(null!, "Employee not found.", true);

            IEnumerable<Payslip> payslips = await _unitOfWork.Payslips.GetByEmployeeIdAsync(employeeId, cancellationToken);
            var dtoList = new List<PayslipDto>();

            foreach (Payslip payslip in payslips)
            {
                PayslipDto dto = _mapper.Map<PayslipDto>(payslip);

                IEnumerable<EmployeeAllowance> employeeAllowances = await _unitOfWork.EmployeeAllowances
                    .GetByEmployeeAndMonthAsync(employeeId, payslip.Month, payslip.Year, cancellationToken);

                IEnumerable<EmployeeDeduction> employeeDeductions = await _unitOfWork.EmployeeDeductions
                    .GetByEmployeeAndMonthAsync(employeeId, payslip.Month, payslip.Year, cancellationToken);

                dto.Allowances = MapEmployeeAllowances(employeeAllowances, payslip.BasicSalary, payslip.Month, payslip.Year);
                dto.Deductions = MapEmployeeDeductions(employeeDeductions, payslip.BasicSalary, payslip.Month, payslip.Year, fixedDeductions);

                dto.TotalAllowances = dto.Allowances.Sum(a => a.Value);
                dto.TotalDeductions = dto.Deductions.Sum(d => d.Value);
                dto.NetSalary = payslip.BasicSalary + dto.TotalAllowances - dto.TotalDeductions;

                dtoList.Add(dto);
            }

            return new Response<IEnumerable<PayslipDto>>(dtoList, string.Empty, false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<PayslipDto>>(null!, $"Error retrieving payslips: {ex.Message}", true);
        }
    }

    public async Task<Response<IEnumerable<PayslipDto>>> GetByMonthAsync(int month, int year, IEnumerable<Deduction> fixedDeductions, CancellationToken cancellationToken)
    {
        try
        {
            IEnumerable<Payslip> payslips = await _unitOfWork.Payslips.GetByMonthAsync(month, year, cancellationToken);
            var dtoList = new List<PayslipDto>();

            foreach (Payslip payslip in payslips)
            {
                PayslipDto dto = _mapper.Map<PayslipDto>(payslip);

                IEnumerable<EmployeeAllowance> employeeAllowances = await _unitOfWork.EmployeeAllowances
                    .GetByEmployeeAndMonthAsync(payslip.EmployeeId, month, year, cancellationToken);

                IEnumerable<EmployeeDeduction> employeeDeductions = await _unitOfWork.EmployeeDeductions
                    .GetByEmployeeAndMonthAsync(payslip.EmployeeId, month, year, cancellationToken);

                dto.Allowances = MapEmployeeAllowances(employeeAllowances, payslip.BasicSalary, month, year);
                dto.Deductions = MapEmployeeDeductions(employeeDeductions, payslip.BasicSalary, month, year, fixedDeductions);

                dto.TotalAllowances = dto.Allowances.Sum(a => a.Value);
                dto.TotalDeductions = dto.Deductions.Sum(d => d.Value);
                dto.NetSalary = payslip.BasicSalary + dto.TotalAllowances - dto.TotalDeductions;

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


}
