using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

public class PdfGeneratorService : IPdfGenerator
{
    public byte[] GeneratePayslipPdf(PayslipDto payslip)
    {
        // Month name for PDF title
        string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(payslip.Month);

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black).FontFamily("Arial"));

                page.Content().Column(col =>
                {
                    col.Spacing(15);

                    // HEADER
                    col.Item().Column(header =>
                    {
                        header.Item().Text("HR Management System")
                            .FontSize(22).Bold().FontColor(Colors.Blue.Darken2);
                        header.Item().Text($"Payslip for {monthName} {payslip.Year}")
                            .FontSize(20).SemiBold().FontColor(Colors.Blue.Darken3);
                        header.Item().Text($"Generated At: {payslip.GeneratedAt:dd/MM/yyyy}")
                            .FontSize(10).FontColor(Colors.Grey.Medium);
                    });

                    // EMPLOYEE INFO BOX
                    col.Item()
                        .Background(Colors.Blue.Lighten5)
                        .Border(1, Colors.Blue.Lighten3)
                        .Padding(12)
                        .Column(emp =>
                        {
                            emp.Item().Text("Employee Info").FontSize(16).Bold().FontColor(Colors.Blue.Darken2);
                            emp.Item().Text($"Employee ID: {payslip.EmployeeId}").FontSize(12);
                            emp.Item().Text($"Employee Name: {payslip.EmployeeName}").FontSize(12);
                            emp.Item().Text($"Basic Salary: {payslip.BasicSalary:C}").FontSize(12);
                        });

                    // ALLOWANCES TABLE
                    col.Item().Text("Allowances")
                        .FontSize(14).Bold().FontColor(Colors.Blue.Darken2);
                    col.Item()
                        .Background(Colors.White)
                        .Border(1, Colors.Blue.Lighten3)
                        .Padding(10)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Blue.Lighten5).Padding(5)
                                      .Text("Allowance").Bold().FontColor(Colors.Blue.Darken3);
                                header.Cell().Background(Colors.Blue.Lighten5).Padding(5)
                                      .Text("Amount").Bold().FontColor(Colors.Blue.Darken3);
                            });

                            if (payslip.Allowances != null)
                            {
                                foreach (var a in payslip.Allowances)
                                {
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Blue.Lighten3)
                                        .Padding(5).Text(a.Name);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Blue.Lighten3)
                                        .Padding(5).Text(a.Amount.ToString("C"));
                                }
                            }

                            // Total Allowance
                            table.Cell().Background(Colors.Blue.Lighten5).Padding(5)
                                .Text("Total").Bold().FontColor(Colors.Blue.Darken3);
                            table.Cell().Background(Colors.Blue.Lighten5).Padding(5)
                                .Text(payslip.TotalAllowances.ToString("C")).Bold().FontColor(Colors.Blue.Darken3);
                        });

                    // DEDUCTIONS TABLE
                    col.Item().Text("Deductions")
                        .FontSize(14).Bold().FontColor(Colors.Blue.Darken2);
                    col.Item()
                        .Background(Colors.White)
                        .Border(1, Colors.Blue.Lighten3)
                        .Padding(10)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Blue.Lighten5).Padding(5)
                                      .Text("Deduction").Bold().FontColor(Colors.Blue.Darken3);
                                header.Cell().Background(Colors.Blue.Lighten5).Padding(5)
                                      .Text("Amount").Bold().FontColor(Colors.Blue.Darken3);
                            });

                            if (payslip.Deductions != null)
                            {
                                foreach (var d in payslip.Deductions)
                                {
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Blue.Lighten3)
                                        .Padding(5).Text(d.Name);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Blue.Lighten3)
                                        .Padding(5).Text(d.Amount.ToString("C"));
                                }
                            }

                            // Total Deduction
                            table.Cell().Background(Colors.Blue.Lighten5).Padding(5)
                                .Text("Total").Bold().FontColor(Colors.Blue.Darken3);
                            table.Cell().Background(Colors.Blue.Lighten5).Padding(5)
                                .Text(payslip.TotalDeductions.ToString("C")).Bold().FontColor(Colors.Blue.Darken3);
                        });

                    // NET SALARY BOX
                    col.Item()
                        .Background(Colors.Blue.Lighten5)
                        .Border(2, Colors.Blue.Darken2)
                        .Padding(15)
                        .Column(net =>
                        {
                            net.Item().Text("Net Salary").FontSize(16).Bold().FontColor(Colors.Blue.Darken3);
                            net.Item().Height(5);
                            net.Item().Text($"{payslip.NetSalary:C}")
                                .FontSize(28).Bold().FontColor(Colors.Blue.Darken4);
                        });

                    // SPACER BEFORE FOOTER
                    col.Item().Height(30);

                    // FOOTER SECTION WITH SIDE-BY-SIDE LAYOUT
                    col.Item().BorderTop(1).BorderColor(Colors.Grey.Lighten1).PaddingTop(15).Column(footer =>
                    {
                        // Company Name
                        footer.Item().AlignCenter().Text("HR Management System")
                            .FontSize(11).Bold().FontColor(Colors.Blue.Darken2);

                        footer.Item().Height(8); // Spacer

                        // Contact info side-by-side (60% left, 40% right)
                        footer.Item().Row(row =>
                        {
                            // Left side - Contact details
                            row.RelativeItem(3).Column(leftCol =>
                            {
                                leftCol.Item().Text("contact@hrmsystem.com")
                                    .FontSize(9).FontColor(Colors.Grey.Darken1);

                                leftCol.Item().Text("+1 (234) 567-890")
                                    .FontSize(9).FontColor(Colors.Grey.Darken1);
                            });

                            // Right side - Address
                            row.RelativeItem(2).Column(rightCol =>
                            {
                                rightCol.Item().Text("123 Business Ave, Suite 100")
                                    .FontSize(9).FontColor(Colors.Grey.Darken1).AlignRight();

                                rightCol.Item().Text("New York, NY 10001")
                                    .FontSize(9).FontColor(Colors.Grey.Darken1).AlignRight();
                            });
                        });

                        footer.Item().Height(10); // Spacer

                        // Footer notes side-by-side
                        footer.Item().Row(row =>
                        {
                            // Left note
                            row.RelativeItem().Column(noteCol =>
                            {
                                noteCol.Item().Text("This is an electronically generated document")
                                    .FontSize(8).FontColor(Colors.Grey.Medium);
                            });

                            // Right note
                            row.RelativeItem().Column(noteCol =>
                            {
                                noteCol.Item().Text("For any discrepancies, contact HR within 7 days")
                                    .FontSize(8).FontColor(Colors.Grey.Medium).AlignRight();
                            });
                        });
                    });
                });
            });
        });

        return document.GeneratePdf();
    }
}
