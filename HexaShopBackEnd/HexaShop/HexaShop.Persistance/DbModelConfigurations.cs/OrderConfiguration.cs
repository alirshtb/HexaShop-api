using HexaShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HexaShop.Persistance.DbModelConfigurations.cs
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.ToTable("Orders");

            builder.Property(o => o.Amount)
                .IsRequired(true)
                .HasDefaultValue(0);

            builder.Property(o => o.DiscountAmount)
                .IsRequired(true)
                .HasDefaultValue(0);

            builder.Property(o => o.TaxAmount)
                .IsRequired(true)
                .HasDefaultValue(0);

            builder.Property(o => o.IsCompleted)
                .IsRequired(true)
                .HasDefaultValue(false);

            builder.Property(o => o.Level)
                .IsRequired(true);

            #region Relations

            builder.HasMany(o => o.LevelLogs)
                .WithOne(l => l.Order)
                .OnDelete(deleteBehavior: DeleteBehavior.ClientNoAction);

            builder.HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            builder.HasOne(o => o.AppUser)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.AppUserId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            builder.HasOne(o => o.Cart)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CartId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            builder.HasMany(o => o.Details)
                .WithOne(od => od.Order)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            #endregion

        }
    }
}
