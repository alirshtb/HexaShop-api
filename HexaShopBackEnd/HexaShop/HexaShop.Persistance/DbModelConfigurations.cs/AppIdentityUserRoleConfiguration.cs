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
    public class AppIdentityUserRoleConfiguration : IEntityTypeConfiguration<AppIdentityUserRole>
    {
        public void Configure(EntityTypeBuilder<AppIdentityUserRole> builder)
        {
                   
            builder.HasOne(hiur => hiur.User)
                   .WithMany(u => u.Roles)
                   .HasForeignKey(hiur => hiur.UserId)
                   .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

            builder.HasOne(hiur => hiur.Role)
                   .WithMany(r => r.Users)
                   .HasForeignKey(hiur => hiur.RoleId)
                   .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

        }
    }
}
