using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Persistance.Repositories
{
    public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
        private readonly HexaShopDbContext _dbContext;

        public DiscountRepository(HexaShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// check discount 
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public bool IsDuplicate(int percent)
        {
            return GetAsQueryable().Any(d => d.Percent == percent);
        }
    }
}
