using System;

namespace HRManagementSystem.Infrastructure.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
      public void Configure(EntityTypeBuilder<Notification> builder)
      {
          builder.ToTable("Notifications");
          builder.HasKey(n => n.Id);

          builder.Property(n => n.Title).IsRequired().HasMaxLength(200);
          builder.Property(n => n.Message).IsRequired().HasMaxLength(1000);
          builder.Property(n => n.Type)
                  .HasConversion<string>()
                  .HasMaxLength(20)
                  .IsRequired();
          builder.Property(n => n.Category)
                  .HasConversion<string>()
                  .HasMaxLength(50)
                  .IsRequired();
          builder.Property(n => n.Priority)
                  .HasConversion<string>()
                  .HasMaxLength(20)
                  .IsRequired();
          builder.Property(n => n.IsRead).IsRequired();
          builder.Property(n => n.ActionUrl).HasMaxLength(500);
          builder.Property(n => n.RelatedEntityType).HasMaxLength(100);

          builder.HasIndex(n => new { n.RecipientUserId, n.IsRead });
      }
  }
