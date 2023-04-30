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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(x => x.Id);

            builder.Property(p => p.IsSuccessful)
                .IsRequired(true)
                .HasDefaultValue(true);

            builder.Property(p => p.Info)
                .IsRequired(true)
                .HasMaxLength(500);

            builder.Property(p => p.Amount)
                .IsRequired(true);

            builder.Property(p => p.FailureReason)
                .IsRequired(false)
                .HasMaxLength(500);

            #region relations 

            builder.HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
