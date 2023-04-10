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
    public class CartItemsConfiguration : IEntityTypeConfiguration<CartItems>
    {
        public void Configure(EntityTypeBuilder<CartItems> builder)
        {
            builder.Property(ci => ci.Price)
                .IsRequired();

            builder.Property(ci => ci.Count)
                .IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);


            #region Relations 

            builder.HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

            builder.HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

            #endregion Relations 
        }
    }
}
