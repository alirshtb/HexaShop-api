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
    public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(d => d.Id);
            builder.ToTable("Discounts");

            builder.Property(d => d.Title)
                .IsRequired(true)
                .HasMaxLength(20);

            builder.Property(d => d.Description)
               .IsRequired(true)
               .HasMaxLength(100);

            builder.Property(d => d.Percent)
                .IsRequired(true)
                .HasDefaultValue(0);

            #region Relations 

            builder.HasMany(d => d.Products)
                .WithOne(p => p.Discount)
                .HasForeignKey(p => p.DiscountId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            #endregion
        }
    }
}
