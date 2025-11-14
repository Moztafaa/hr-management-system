namespace HRManagementSystem.Infrastructure.Configurations
{
    public class EmployeeDeductionConfiguration : IEntityTypeConfiguration<EmployeeDeduction>
    {
        public void Configure(EntityTypeBuilder<EmployeeDeduction> builder)
        {
            builder.ToTable("EmployeeDeductions");

            builder.HasKey(ed => ed.Id);

            builder.Property(ed => ed.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

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
