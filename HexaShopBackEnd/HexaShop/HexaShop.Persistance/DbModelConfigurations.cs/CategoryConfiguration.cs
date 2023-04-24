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

            builder.HasQueryFilter(x => !x.IsDeleted);


            #region Relations 

            builder.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .OnDelete(deleteBehavior: DeleteBehavior.Cascade);


            #endregion Relations

        }
    }
}
