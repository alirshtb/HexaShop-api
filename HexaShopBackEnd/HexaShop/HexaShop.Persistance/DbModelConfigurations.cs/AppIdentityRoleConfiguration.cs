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
    public class AppIdentityRoleConfiguration : IEntityTypeConfiguration<AppIdentityRole>
    {
        public void Configure(EntityTypeBuilder<AppIdentityRole> builder)
        {
            builder.Property(r => r.DisplayName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(r => r.Users)
                .WithOne()
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
