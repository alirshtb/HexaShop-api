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
    public class ImageSourceConfiguration : IEntityTypeConfiguration<ImageSource>
    {
        public void Configure(EntityTypeBuilder<ImageSource> builder)
        {
            builder.ToTable("ImageSources");
            builder.HasKey(x => x.Id);

            builder.Property(ims => ims.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(ims => ims.Address)
                .IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);


            #region Relations 

            builder.HasOne(ims => ims.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(ims => ims.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion Relations 
        }
    }
}
