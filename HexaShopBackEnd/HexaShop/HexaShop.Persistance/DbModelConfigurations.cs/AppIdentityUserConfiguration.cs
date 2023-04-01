using HexaShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Persistance.DbModelConfigurations
{
    public class AppIdentityUserConfiguration : IEntityTypeConfiguration<AppIdentityUser>
    {
        public void Configure(EntityTypeBuilder<AppIdentityUser> builder)
        {
            builder.Property(u => u.FirstName)
                .IsRequired()
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(p => p.Email).IsUnique();

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasMany(u => u.Roles)
                .WithOne()
                .HasForeignKey(hiur => hiur.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
