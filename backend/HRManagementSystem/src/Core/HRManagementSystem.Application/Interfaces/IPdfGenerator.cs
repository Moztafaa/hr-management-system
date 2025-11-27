namespace HRManagementSystem.Application.Interfaces;

public interface IPdfGenerator
{
    byte[] GeneratePayslipPdf(PayslipDto payslip);
}
