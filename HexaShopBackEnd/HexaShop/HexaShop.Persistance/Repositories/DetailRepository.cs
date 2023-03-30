using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Persistance.Repositories
{
    public class DetailRepository : GenericRepository<Detail>, IDetailRepsitory
    {
        private readonly HexaShopDbContext _dbContext;

        public DetailRepository(HexaShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
