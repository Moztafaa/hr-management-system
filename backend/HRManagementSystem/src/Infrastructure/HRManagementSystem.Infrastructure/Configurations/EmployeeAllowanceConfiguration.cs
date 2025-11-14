namespace HRManagementSystem.Infrastructure.Configurations
{
    public class EmployeeAllowanceConfiguration : IEntityTypeConfiguration<EmployeeAllowance>
    {
        public void Configure(EntityTypeBuilder<EmployeeAllowance> builder)
        {
            builder.ToTable("EmployeeAllowances");

            builder.HasKey(ea => ea.Id);

            builder.Property(ea => ea.Amount)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

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
