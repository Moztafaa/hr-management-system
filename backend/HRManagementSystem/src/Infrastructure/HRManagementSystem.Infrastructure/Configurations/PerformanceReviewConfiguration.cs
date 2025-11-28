using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Configurations;
public class PerformanceReviewConfiguration : IEntityTypeConfiguration<PerformanceReview>
{
    public void Configure(EntityTypeBuilder<PerformanceReview> b)
    {
        b.HasOne(r => r.Employee)
         .WithMany()
         .HasForeignKey(r => r.EmployeeId)
         .OnDelete(DeleteBehavior.NoAction);

        b.HasMany(r => r.Goals)
         .WithOne(g => g.Review)
         .HasForeignKey(g => g.PerformanceReviewId)
         .OnDelete(DeleteBehavior.Cascade);
    }
}
