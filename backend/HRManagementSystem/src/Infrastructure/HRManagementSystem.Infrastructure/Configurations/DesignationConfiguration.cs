using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Configurations;
public class DesignationConfiguration : IEntityTypeConfiguration<Designation>
{
    public void Configure(EntityTypeBuilder<Designation> builder){
        builder.ToTable("Designations");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.Title).IsRequired().HasMaxLength(100);
        builder.Property(d => d.Description).HasMaxLength(500);

        builder.HasMany(d => d.Employees)
               .WithOne(e => e.Designation)
               .HasForeignKey(e => e.DesignationId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
