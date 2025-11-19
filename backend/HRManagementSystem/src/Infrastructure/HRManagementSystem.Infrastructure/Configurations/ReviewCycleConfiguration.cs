using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Configurations;
public class ReviewCycleConfiguration : IEntityTypeConfiguration<ReviewCycle>
{
    public void Configure(EntityTypeBuilder<ReviewCycle> b)
    {
        b.Property(x => x.Name).IsRequired().HasMaxLength(150);
        b.HasMany(x => x.Reviews)
         .WithOne(r => r.ReviewCycle)
         .HasForeignKey(r => r.ReviewCycleId)
         .OnDelete(DeleteBehavior.NoAction);
    }
}
