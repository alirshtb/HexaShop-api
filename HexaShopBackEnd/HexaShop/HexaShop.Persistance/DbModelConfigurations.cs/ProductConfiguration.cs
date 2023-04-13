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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasKey(p => p.Id);

            builder.ToTable("Products");

            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnName("Title")
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnName("Description");

            builder.Property(p => p.Score)
                .HasDefaultValue(0)
                .HasMaxLength(5);

            builder.Property(p => p.MainImage)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(p => p.IsSpecial)
                .IsRequired()
                .HasDefaultValue(false);


            builder.HasQueryFilter(x => !x.IsDeleted);

            #region Relations 

            builder.HasMany(p => p.Details)
                .WithOne(d => d.Product)
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(p => p.Images)
                .WithOne(ims => ims.Product)
                .HasForeignKey(ims => ims.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(p => p.Categories)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            builder.HasMany(p => p.CartItems)
                .WithOne(ci => ci.Product)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            builder.HasOne(p => p.Discount)
                .WithMany(d => d.Products)
                .HasForeignKey(p => p.DiscountId)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

            #endregion Relations
        }
    }
}
