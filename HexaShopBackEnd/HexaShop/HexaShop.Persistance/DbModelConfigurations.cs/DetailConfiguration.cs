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
    public class DetailConfiguration : IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> builder)
        {
            builder.ToTable("Details");
            
            builder.HasKey(x => x.Id);

            builder.Property(d => d.Key)
                .IsRequired();

            builder.Property(d => d.Value)
                .IsRequired();

            #region Relations 

            builder.HasOne(d => d.Product)
                .WithMany(p => p.Details)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.NoAction);


            #endregion Relations
        }
    }
}
