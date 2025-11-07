using HRManagementSystem.Domain.Entities;

namespace HRManagementSystem.Application.Services;
public class PayslipService : IPayslipService
{
    private readonly IPayslipRepository _payslipRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public PayslipService(IPayslipRepository payslipRepository, IEmployeeRepository employeeRepository)
    {
        _payslipRepository = payslipRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task GeneratePayslipAsync(int employeeId, int month, int year)
    {
        if (await _payslipRepository.ExistsAsync(employeeId, month, year))
        {
            throw new InvalidOperationException("Payslip already exists!");
        }

        Employee? employee = await _employeeRepository.GetByIdAsync(employeeId) ?? throw new KeyNotFoundException("Employee not found!");

        decimal basicSalary = employee.BasicSalary;
        decimal totalAllowances = 0m;         // to be calculated  
        decimal totalDeductions = 0m;         // to be calculated
        decimal netSalary = basicSalary + totalAllowances - totalDeductions;

        var payslip = new Payslip
        {
            EmployeeId = employeeId,
            BasicSalary = basicSalary,
            TotalAllowances = totalAllowances,
            TotalDeductions = totalDeductions,
            NetSalary = netSalary,
            Month = month,
            Year = year
        };

        await _payslipRepository.AddAsync(payslip);
    }

    public async Task GeneratePayslipsForMonthAsync(int month, int year)
    {
        IEnumerable<Employee> employees = await _employeeRepository.GetAllAsync();

        IEnumerable<int> employeeIds = employees.Select(emp => emp.Id);

        foreach (int id in employeeIds)
        {
            bool exists = await _payslipRepository.ExistsAsync(id, month, year);

            if (!exists)
            {
                await GeneratePayslipAsync(id, month, year);
            }
        }
    }
}
