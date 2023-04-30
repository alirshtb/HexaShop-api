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
    public class OrderLevelLogConfiguration : IEntityTypeConfiguration<OrderLevelLog>
    {
        public void Configure(EntityTypeBuilder<OrderLevelLog> builder)
        {
            builder.HasKey(l => l.Id);
            builder.ToTable("OrderLevelLogs");

            builder.Property(l => l.Title)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(l => l.CurrentLevel)
                .IsRequired(true);

            builder.Property(l => l.NextLevel)
                .IsRequired(true);


            #region Relations

            builder.HasOne(l => l.Order)
                .WithMany(o => o.LevelLogs)
                .HasForeignKey(l => l.OrderId)
                .OnDelete(deleteBehavior: DeleteBehavior.ClientNoAction);

            #endregion


        }
    }
}
