using HexaShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
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
            builder.ToTable(nameof(Discount));
            builder.Property(d => d.Title)
                .IsRequired(true)
                .HasMaxLength(30);

            builder.Property(d => d.Description)
                .IsRequired(false)
                .HasMaxLength(100);

            #region Relations 

            builder.HasMany(d => d.Products)
                .WithOne(p => p.Discount)
                .HasForeignKey(p => p.DiscountId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            #endregion
        }
    }
}
