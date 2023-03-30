using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Persistance
{
    public class HexaShopDbContextFactory : IDesignTimeDbContextFactory<HexaShopDbContext>
    {
        public HexaShopDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<HexaShopDbContext>();
            //var connectionString = "Server = (localdb)\\mssqllocaldb; Database = HexaShopDb;Trusted_Connection = true; MultipleActiveResultSets=true;";
            var connectionString = "Server = (localdb)\\mssqllocaldb; Database = HexaShopDb; Integrated Security = true; MultipleActiveResultSets = true;";

            optionBuilder.UseSqlServer(connectionString, builder =>
            {
            });

            return new HexaShopDbContext(optionBuilder.Options);
        }
    }
}
