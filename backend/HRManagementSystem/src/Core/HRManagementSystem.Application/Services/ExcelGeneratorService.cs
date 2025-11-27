using ClosedXML.Excel;


namespace HRManagementSystem.Infrastructure.Services;

public class ExcelGeneratorService : IExcelGenerator
{
    public byte[] GenerateMonthlyPayslipsExcel(IEnumerable<PayslipDto> payslips, int month, int year)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add($"Payslips_{month}-{year}");

        // Headers
        worksheet.Cell(1, 1).Value = "Employee ID";
        worksheet.Cell(1, 2).Value = "Employee Name";
        worksheet.Cell(1, 3).Value = "Month";
        worksheet.Cell(1, 4).Value = "Year";
        worksheet.Cell(1, 5).Value = "Basic Salary";
        worksheet.Cell(1, 6).Value = "Total Allowances";
        worksheet.Cell(1, 7).Value = "Total Deductions";
        worksheet.Cell(1, 8).Value = "Net Salary";

        worksheet.Range("A1:G1").Style.Font.Bold = true;

        // Rows
        int row = 2;
        foreach (var p in payslips.OrderBy(p => p.EmployeeId))
        {
            worksheet.Cell(row, 1).Value = p.EmployeeId;
            worksheet.Cell(row, 2).Value = p.EmployeeName;
            worksheet.Cell(row, 3).Value = p.Month;
            worksheet.Cell(row, 4).Value = p.Year;
            worksheet.Cell(row, 5).Value = p.BasicSalary;
            worksheet.Cell(row, 6).Value = p.TotalAllowances;
            worksheet.Cell(row, 7).Value = p.TotalDeductions;
            worksheet.Cell(row, 8).Value = p.NetSalary;
            row++;
        }

        worksheet.Columns().AdjustToContents();

        using var ms = new MemoryStream();
        workbook.SaveAs(ms);

        return ms.ToArray();
    }
}
