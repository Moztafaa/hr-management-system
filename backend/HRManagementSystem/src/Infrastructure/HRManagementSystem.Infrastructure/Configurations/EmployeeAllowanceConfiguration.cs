namespace HRManagementSystem.Infrastructure.Configurations
{
    public class EmployeeAllowanceConfiguration : IEntityTypeConfiguration<EmployeeAllowance>
    {
        public void Configure(EntityTypeBuilder<EmployeeAllowance> builder)
        {
            builder.ToTable("EmployeeAllowances");

            builder.HasKey(ea => new { ea.EmployeeId, ea.AllowanceId });

            builder.Property(ea => ea.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired(false);

            builder.Property(ea => ea.IsPercentage).IsRequired(false);
            builder.Property(ea => ea.Recurrence)
                   .HasConversion<string>()
                   .IsRequired();
            builder.Property(ea => ea.StartDate).IsRequired(false);
            builder.Property(ea => ea.EndDate).IsRequired(false);
            builder.Property(ea => ea.CreatedAt).IsRequired();
            builder.Property(ea => ea.UpdatedAt).IsRequired(false);

            builder.HasOne(ea => ea.Employee)
                   .WithMany(e => e.EmployeeAllowances)
                   .HasForeignKey(ea => ea.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ea => ea.Allowance)
                   .WithMany(a => a.EmployeeAllowances)
                   .HasForeignKey(ea => ea.AllowanceId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
