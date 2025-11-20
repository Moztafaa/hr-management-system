namespace HRManagementSystem.Infrastructure.Configurations
{
    public class PayslipConfiguration : IEntityTypeConfiguration<Payslip>
    {
        public void Configure(EntityTypeBuilder<Payslip> builder)
        {
            builder.ToTable("Payslips");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.BasicSalary)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.TotalAllowances)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.TotalDeductions)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.NetSalary)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Month)
                .IsRequired();

            builder.Property(p => p.Year)
                .IsRequired();

            builder.Property(p => p.GeneratedAt)
                .IsRequired();

            builder.HasOne(p => p.Employee)
                   .WithMany(e => e.Payslips)
                   .HasForeignKey(p => p.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);

            //builder.HasMany(p => p.Allowances)
            //       .WithMany(a => a.Payslips)
            //       .UsingEntity(j => j.ToTable("PayslipAllowances"));

            //builder.HasMany(p => p.Deductions)
            //       .WithMany(d => d.Payslips)
            //       .UsingEntity(j => j.ToTable("PayslipDeductions"));
        }
    }
}
