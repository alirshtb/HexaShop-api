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
    public class HistoryConfiguration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.HasKey(h => h.Id);

            builder.ToTable(nameof(History));

            //builder.Property(h => h.UserId)
            //    .IsRequired(false);

            builder.Property(h => h.DateCreated)
                .IsRequired();

            builder.Property(h => h.State)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(h => h.PreviousValue)
                .IsRequired(false);

            builder.Property(h => h.NextValue)
                .IsRequired(false);

            builder.Property(h => h.ChangeDate)
                .IsRequired(true);

            builder.Property(h => h.ColumnName)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(h => h.TableName)
                .IsRequired(true)
                .HasMaxLength(100);

            //builder.Property(h => h.UserFullName)
            //    .IsRequired(false)
            //    .HasMaxLength(100);

            builder.Property(h => h.RecordId)
                .IsRequired(true);
        }
    }
}
