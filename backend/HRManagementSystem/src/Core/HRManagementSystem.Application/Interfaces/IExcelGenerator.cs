namespace HRManagementSystem.Application.Interfaces;

public interface IExcelGenerator
{
    byte[] GenerateMonthlyPayslipsExcel(IEnumerable<PayslipDto> payslips, int month, int year);
}
