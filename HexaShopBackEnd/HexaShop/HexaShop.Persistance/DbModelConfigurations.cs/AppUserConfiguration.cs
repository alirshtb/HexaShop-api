using HexaShop.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Persistance.DbModelConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {


            builder.Property(hu => hu.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(hu => hu.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(hu => hu.Email)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(hu => hu.Gender)
                   .IsRequired();

            builder.Property(hu => hu.Mobile)
                   .IsRequired();

            builder.Property(hu => hu.DateOfBirth)
                   .IsRequired(false);


        }
    }
}
