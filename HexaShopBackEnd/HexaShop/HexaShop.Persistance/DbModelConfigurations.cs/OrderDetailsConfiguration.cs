using HexaShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Persistance.DbModelConfigurations.cs
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetails>
    {
        public void Configure(EntityTypeBuilder<OrderDetails> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(od => od.Id);

            builder.Property(od => od.Count)
                .IsRequired(true)
                .HasDefaultValue(0);

            builder.Property(od => od.UnitPrice)
                .IsRequired(true)
                .HasDefaultValue(0);

            builder.Property(od => od.UnitDiscount)
                .IsRequired(true)
                .HasDefaultValue(0);

            builder.Property(od => od.TotalDiscount)
                .IsRequired(true)
                .HasDefaultValue(0);

            builder.Property(od => od.TotalAmount)
                .IsRequired(true)
                .HasDefaultValue(0);

            #region Relations

            builder.HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .IsRequired(true)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

            builder.HasOne(od => od.Order)
                .WithMany(o => o.Details)
                .HasForeignKey(od => od.OrderId)
                .IsRequired(true)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            #endregion

        }
    }
}
