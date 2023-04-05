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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.ParentCategoryId)
                .HasDefaultValue(null);

            builder.Property(p => p.Image)
                .IsRequired();

            #region Relations 

            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            //builder.HasMany(c => c.ChildCategories)
            //    .WithOne(chc => chc.ParentCategory)
            //    .HasForeignKey(chc => chc.ParentCategoryId)
            //    .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            //builder.HasOne(chc => chc.ParentCategory)
            //    .WithMany(c => c.ChildCategories)
            //    .HasForeignKey(chc => chc.ParentCategoryId)
            //    .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

            #endregion Relations

        }
    }
}
