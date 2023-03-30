using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Persistance.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly HexaShopDbContext _dbContext;

        public ProductRepository(HexaShopDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }


    }
}
