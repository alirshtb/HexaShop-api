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
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.Property(c => c.BrowserId)
                .IsRequired();

            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(c => c.AppUserId)
                .IsRequired(false);

            #region Relations 

            builder.HasMany(c => c.Items)
                .WithOne(i => i.Cart)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(c => c.User)
                .WithMany(u => u.Cart)
                .HasForeignKey(c => c.AppUserId)
                .IsRequired(false)
                .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Cart)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            #endregion Relations 
        }
    }
}
