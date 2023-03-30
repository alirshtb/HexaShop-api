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
    public class ProductInCategoryConfiguration : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.ToTable(nameof(ProductInCategory));
            builder.HasKey(pc => pc.Id);

            #region Relations

            builder.HasOne(pc => pc.Product)
                .WithMany(p => p.Categories)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            builder.HasOne(pc => pc.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            #endregion Relations 
        }
    }
}
