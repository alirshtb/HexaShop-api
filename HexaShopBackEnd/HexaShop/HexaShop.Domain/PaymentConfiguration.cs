using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("Payments");

            builder.Property(p => p.IsSuccessful)
                .IsRequired(true)
                .HasDefaultValue(false);

            builder.Property(p => p.Amount)
                .IsRequired(true)
                .HasDefaultValue(0);

            builder.Property(p => p.FailureReason)
                .IsRequired(false);

            builder.Property(p => p.Info)
                .IsRequired(false);


            #region Relations

            builder.HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);


            #endregion

        }
    }
}
