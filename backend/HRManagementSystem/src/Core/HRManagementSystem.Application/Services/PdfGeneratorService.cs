using QuestPDF.Fluent;
using QuestPDF.Helpers;

public class PdfGeneratorService : IPdfGenerator
{
    public byte[] GeneratePayslipPdf(PayslipDto payslip)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);

                page.Content().Column(col =>
                {
                    col.Item().Text("Employee Payslip").FontSize(20).Bold();
                    col.Item().Text($"Month: {payslip.Month} | Year: {payslip.Year}");
                    col.Item().Text($"Employee ID: {payslip.EmployeeId}");
                    col.Item().Text($"Basic Salary: {payslip.BasicSalary:C}");
                    col.Item().PaddingVertical(10);

                    // Allowances
                    col.Item().Text("Allowances:").Bold();
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Allowance").Bold();
                            header.Cell().Text("Amount").Bold();
                        });

                        foreach (var a in payslip.Allowances)
                        {
                            table.Cell().Text(a.Name);
                            table.Cell().Text(a.Amount.ToString("C"));
                        }
                    });

                    col.Item().PaddingVertical(10);

                    // Deductions
                    col.Item().Text("Deductions:").Bold();
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Deduction").Bold();
                            header.Cell().Text("Amount").Bold();
                        });

                        foreach (var d in payslip.Deductions)
                        {
                            table.Cell().Text(d.Name);
                            table.Cell().Text(d.Amount.ToString("C"));
                        }
                    });

                    col.Item().PaddingVertical(10);

                    col.Item().Text($"Net Salary: {payslip.NetSalary:C}")
                        .FontSize(16).Bold();
                });
            });
        });

        return document.GeneratePdf();
    }
}
