namespace HRManagementSystem.Infrastructure.Configurations
{
    public class EmployeeDeductionConfiguration : IEntityTypeConfiguration<EmployeeDeduction>
    {
        public void Configure(EntityTypeBuilder<EmployeeDeduction> builder)
        {
            builder.ToTable("EmployeeDeductions");

            builder.HasKey(ed => new { ed.EmployeeId, ed.DeductionId });

            builder.Property(ed => ed.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired(false);

            builder.Property(ed => ed.IsPercentage).IsRequired(false);
            builder.Property(ed => ed.Recurrence)
                   .HasConversion<string>()
                   .IsRequired();
            builder.Property(ed => ed.StartDate).IsRequired(false);
            builder.Property(ed => ed.EndDate).IsRequired(false);
            builder.Property(ed => ed.CreatedAt).IsRequired();
            builder.Property(ed => ed.UpdatedAt).IsRequired(false);

            builder.HasOne(ed => ed.Employee)
                   .WithMany(e => e.EmployeeDeductions)
                   .HasForeignKey(ed => ed.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ed => ed.Deduction)
                   .WithMany(d => d.EmployeeDeductions)
                   .HasForeignKey(ed => ed.DeductionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
