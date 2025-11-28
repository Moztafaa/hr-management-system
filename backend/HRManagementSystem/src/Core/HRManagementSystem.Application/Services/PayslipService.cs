using HRManagementSystem.Application.DTOs.File;
using HRManagementSystem.Application.DTOs.Notification;
using HRManagementSystem.Domain.Enums.Notification;

namespace HRManagementSystem.Application.Services;

public class PayslipService : IPayslipService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPdfGenerator _pdfGenerator;
    private readonly IExcelGenerator _excelGenerator;
    private readonly IEmailService _emailService;
    private readonly INotificationService _notificationService;

    public PayslipService(IUnitOfWork unitOfWork, IMapper mapper, IPdfGenerator pdfGenerator, IExcelGenerator excelGenerator, IEmailService emailService, INotificationService notificationService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _pdfGenerator = pdfGenerator;
        _excelGenerator = excelGenerator;
        _emailService = emailService;
        _notificationService = notificationService;
    }

    // ------------------------------------------------------------------------
    // MAIN GENERATION LOGIC
    // ------------------------------------------------------------------------
    public async Task<Response<PayslipDto>> GeneratePayslipAsync(
        int employeeId, int month, int year, CancellationToken cancellationToken)
    {
        try
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(employeeId, cancellationToken);
            if (employee == null)
                return new Response<PayslipDto>(null!, "Employee not found.", true);

            var existing = await _unitOfWork.Payslips.GetByEmployeeAndMonthAsync(
                employeeId, month, year, cancellationToken);

            var (allowances, deductions) = await LoadActiveBenefitsAsync(employeeId, month, year, cancellationToken);

            decimal totalAllowances = allowances.Sum(a =>
            {
                bool useEmployeeValue = a.Amount.HasValue && a.IsPercentage.HasValue;

                decimal amount = useEmployeeValue
                    ? a.Amount!.Value
                    : a.Allowance.Amount;

                bool isPercentage = useEmployeeValue
                    ? a.IsPercentage!.Value
                    : a.Allowance.IsPercentage;

                return isPercentage
                    ? (employee.BasicSalary * (amount / 100m))
                    : amount;
            });

            decimal totalDeductions = deductions.Sum(d =>
            {
                bool useEmployeeValue = d.Amount.HasValue && d.IsPercentage.HasValue;

                decimal amount = useEmployeeValue
                    ? d.Amount!.Value
                    : d.Deduction.Amount;

                bool isPercentage = useEmployeeValue
                    ? d.IsPercentage!.Value
                    : d.Deduction.IsPercentage;

                return isPercentage
                    ? (employee.BasicSalary * (amount / 100m))
                    : amount;
            });


            decimal netSalary = employee.BasicSalary + totalAllowances - totalDeductions;

            Payslip payslip = existing ?? new Payslip { EmployeeId = employeeId, Month = month, Year = year };

            payslip.BasicSalary = employee.BasicSalary;
            payslip.TotalAllowances = totalAllowances;
            payslip.TotalDeductions = totalDeductions;
            payslip.NetSalary = netSalary;
            payslip.GeneratedAt = DateTime.UtcNow;

            if (existing != null)
                await _unitOfWork.Payslips.UpdateAsync(payslip, cancellationToken);
            else
                await _unitOfWork.Payslips.AddAsync(payslip, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Send payslip notification email to employee
            try
            {
                if (!string.IsNullOrEmpty(employee.Email))
                {
                    await _emailService.SendPayslipEmailAsync(
                        employee.Email,
                        $"{employee.FirstName} {employee.LastName}",
                        $"{month}/{year}",
                        netSalary,
                        cancellationToken);
                }

                // Create notification for employee
                if (!string.IsNullOrEmpty(employee.ApplicationUserId))
                {
                    await _notificationService.CreateNotificationAsync(new CreateNotificationDto
                    {
                        RecipientUserId = employee.ApplicationUserId,
                        Title = "Payslip Generated",
                        Message = $"Your payslip for {month}/{year} has been generated. Net Salary: ${netSalary:N2}",
                        Type = NotificationType.Info,
                        Category = NotificationCategory.Payroll,
                        Priority = NotificationPriority.Normal,
                        RelatedEntityId = payslip.Id,
                        RelatedEntityType = "Payslip"
                    }, cancellationToken);
                }
            }
            catch (Exception emailEx)
            {
                Console.WriteLine($"Failed to send payslip email: {emailEx.Message}");
            }

            return new Response<PayslipDto>(MapPayslip(payslip, allowances, deductions), "", false);
        }
        catch (Exception ex)
        {
            return new Response<PayslipDto>(null!, $"Error generating payslip: {ex.Message}", true);
        }
    }

    // ------------------------------------------------------------------------
    // GENERATE FOR ALL EMPLOYEES
    // ------------------------------------------------------------------------
    public async Task<Response<IEnumerable<PayslipDto>>> GeneratePayslipsForMonthAsync(
        int month, int year, CancellationToken cancellationToken)
    {
        try
        {
            var employees = await _unitOfWork.Employees.GetAllAsync(cancellationToken);
            var result = new List<PayslipDto>();

            foreach (var emp in employees)
            {
                var payslip = await GeneratePayslipAsync(emp.Id, month, year, cancellationToken);
                if (!payslip.HasError)
                    result.Add(payslip.Data);
            }

            return new Response<IEnumerable<PayslipDto>>(result, "", false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<PayslipDto>>(null!, $"Error generating payslips: {ex.Message}", true);
        }
    }

    // ------------------------------------------------------------------------
    // GET BY EMPLOYEE
    // ------------------------------------------------------------------------
    public async Task<Response<IEnumerable<PayslipDto>>> GetByEmployeeAsync(int employeeId, CancellationToken cancellationToken)
    {
        try
        {
            var payslips = await _unitOfWork.Payslips.GetByEmployeeIdAsync(employeeId, cancellationToken);
            var dtoList = new List<PayslipDto>();

            foreach (var payslip in payslips)
            {
                var (allowances, deductions) =
                    await LoadActiveBenefitsAsync(employeeId, payslip.Month, payslip.Year, cancellationToken);

                dtoList.Add(MapPayslip(payslip, allowances, deductions));
            }

            return new Response<IEnumerable<PayslipDto>>(dtoList, "", false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<PayslipDto>>(null!, $"Error retrieving payslips: {ex.Message}", true);
        }
    }

    // ------------------------------------------------------------------------
    // GET SINGLE PAYSPLIP
    // ------------------------------------------------------------------------
    public async Task<Response<PayslipDto>> GetEmployeePayslipForMonthAsync(
        int employeeId, int month, int year, CancellationToken cancellationToken)
    {
        try
        {
            var payslip = await _unitOfWork.Payslips.GetByEmployeeAndMonthAsync(
                employeeId, month, year, cancellationToken);

            if (payslip == null)
                return new Response<PayslipDto>(null!, "Payslip not found.", true);

            var (allowances, deductions) =
                await LoadActiveBenefitsAsync(employeeId, month, year, cancellationToken);

            return new Response<PayslipDto>(MapPayslip(payslip, allowances, deductions), "", false);
        }
        catch (Exception ex)
        {
            return new Response<PayslipDto>(null!, $"Error retrieving payslip: {ex.Message}", true);
        }
    }

    // ------------------------------------------------------------------------
    // GET BY MONTH
    // ------------------------------------------------------------------------
    public async Task<Response<IEnumerable<PayslipDto>>> GetByMonthAsync(int month, int year, CancellationToken cancellationToken)
    {
        try
        {
            var payslips = await _unitOfWork.Payslips.GetByMonthAsync(month, year, cancellationToken);
            var result = new List<PayslipDto>();

            foreach (var payslip in payslips)
            {
                var (allowances, deductions) =
                    await LoadActiveBenefitsAsync(payslip.EmployeeId, month, year, cancellationToken);

                result.Add(MapPayslip(payslip, allowances, deductions));
            }

            return new Response<IEnumerable<PayslipDto>>(result, "", false);
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<PayslipDto>>(null!, $"Error retrieving payslips: {ex.Message}", true);
        }
    }

    // ------------------------------------------------------------------------
    // DELETE
    // ------------------------------------------------------------------------
    public async Task<Response<bool>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var payslip = await _unitOfWork.Payslips.GetByIdAsync(id, cancellationToken);
            if (payslip == null)
                return new Response<bool>(false, "Payslip not found.", true);

            await _unitOfWork.Payslips.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Response<bool>(true, "", false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error deleting payslip: {ex.Message}", true);
        }
    }

    // ------------------------------------------------------------------------
    // PRIVATE HELPERS
    // ------------------------------------------------------------------------
    private async Task<(IEnumerable<EmployeeAllowance>, IEnumerable<EmployeeDeduction>)> LoadActiveBenefitsAsync(
        int employeeId, int month, int year, CancellationToken cancellationToken)
    {
        var allowances = await _unitOfWork.Payslips.GetActiveAllowancesAsync(employeeId, month, year, cancellationToken);
        var deductions = await _unitOfWork.Payslips.GetActiveDeductionsAsync(employeeId, month, year, cancellationToken);

        // Use Employee values if available; fallback to default Allowance/Deduction values if null
        var finalizedAllowances = allowances.Select(a =>
        {
            return new EmployeeAllowance
            {
                EmployeeId = a.EmployeeId,
                AllowanceId = a.AllowanceId,
                Amount = a.Amount ?? a.Allowance.Amount,
                IsPercentage = a.IsPercentage ?? a.Allowance.IsPercentage,
                Allowance = a.Allowance
            };
        }).ToList();

        var finalizedDeductions = deductions.Select(d =>
        {
            return new EmployeeDeduction
            {
                EmployeeId = d.EmployeeId,
                DeductionId = d.DeductionId,
                Amount = d.Amount ?? d.Deduction.Amount,
                IsPercentage = d.IsPercentage ?? d.Deduction.IsPercentage,
                Deduction = d.Deduction
            };
        }).ToList();

        return (finalizedAllowances, finalizedDeductions);
    }

    private PayslipDto MapPayslip(Payslip payslip, IEnumerable<EmployeeAllowance> allowances, IEnumerable<EmployeeDeduction> deductions)
    {
        return new PayslipDto
        {
            Id = payslip.Id,
            EmployeeId = payslip.EmployeeId,
            EmployeeName = payslip.Employee.FirstName + " " + payslip.Employee.LastName,
            BasicSalary = payslip.BasicSalary,
            TotalAllowances = payslip.TotalAllowances,
            TotalDeductions = payslip.TotalDeductions,
            NetSalary = payslip.NetSalary,
            Month = payslip.Month,
            Year = payslip.Year,
            GeneratedAt = payslip.GeneratedAt,
            Allowances = allowances.Select(a => new AllowanceDto
            {
                Id = a.AllowanceId,
                Name = a.Allowance.Name,
                Amount = (decimal)a.Amount,          // Will show EmployeeAllowance.Amount if exists, otherwise default Allowance.Amount
                IsPercentage = (bool)a.IsPercentage
            }).ToList(),
            Deductions = deductions.Select(d => new DeductionDto
            {
                Id = d.DeductionId,
                Name = d.Deduction.Name,
                Amount = (decimal)d.Amount,          // Will show EmployeeDeduction.Amount if exists, otherwise default Deduction.Amount
                IsPercentage = (bool)d.IsPercentage
            }).ToList()
        };
    }

    public async Task<Response<PayslipDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var payslip = await _unitOfWork.Payslips.GetByIdAsync(id, cancellationToken);
            if (payslip == null)
                return new Response<PayslipDto>(null!, "Payslip not found.", true);

            var (allowances, deductions) = await LoadActiveBenefitsAsync(
                payslip.EmployeeId, payslip.Month, payslip.Year, cancellationToken);

            var dto = MapPayslip(payslip, allowances, deductions);

            return new Response<PayslipDto>(dto, "", false);
        }
        catch (Exception ex)
        {
            return new Response<PayslipDto>(null!, $"Error retrieving payslip: {ex.Message}", true);
        }
    }

    public async Task<Response<bool>> ExistsAsync(int employeeId, int month, int year, CancellationToken cancellationToken)
    {
        try
        {
            var payslip = await _unitOfWork.Payslips.GetByEmployeeAndMonthAsync(
                employeeId, month, year, cancellationToken);

            bool exists = payslip != null;

            return new Response<bool>(exists, "", false);
        }
        catch (Exception ex)
        {
            return new Response<bool>(false, $"Error checking payslip existence: {ex.Message}", true);
        }
    }

    public async Task<Response<FileExportDto>> ExportToPdfAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var payslipResponse = await GetByIdAsync(id, cancellationToken);
            if (payslipResponse.HasError)
                return new Response<FileExportDto>(null!, payslipResponse.ErrorMessage, true);

            var payslip = payslipResponse.Data;

            // PDF bytes created by infrastructure
            var pdfBytes = _pdfGenerator.GeneratePayslipPdf(payslip);

            var file = new FileExportDto
            {
                FileName = $"Payslip_{payslip.EmployeeId}_{payslip.Month}-{payslip.Year}.pdf",
                ContentType = "application/pdf",
                FileBytes = pdfBytes
            };

            return new Response<FileExportDto>(file, "", false);
        }
        catch (Exception ex)
        {
            return new Response<FileExportDto>(null!, $"Error exporting PDF: {ex.Message}", true);
        }
    }

    public async Task<Response<FileExportDto>> ExportMonthToExcelAsync(
        int month, int year, CancellationToken cancellationToken)
    {
        try
        {
            var payslipsResponse = await GetByMonthAsync(month, year, cancellationToken);
            if (payslipsResponse.HasError)
                return new Response<FileExportDto>(null!, payslipsResponse.ErrorMessage, true);

            var payslips = payslipsResponse.Data.ToList();

            // Create Excel file
            var excelBytes = _excelGenerator.GenerateMonthlyPayslipsExcel(payslips, month, year);

            var file = new FileExportDto
            {
                FileName = $"Payslips_{month}-{year}.xlsx",
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileBytes = excelBytes
            };

            return new Response<FileExportDto>(file, "", false);
        }
        catch (Exception ex)
        {
            return new Response<FileExportDto>(null!, $"Error exporting Excel: {ex.Message}", true);
        }
    }

}
