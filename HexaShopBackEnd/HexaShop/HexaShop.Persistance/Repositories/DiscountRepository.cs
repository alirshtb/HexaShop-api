using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Domain;

namespace HexaShop.Persistance.Repositories
{
    public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
        private readonly HexaShopDbContext _db;

        public DiscountRepository(HexaShopDbContext db) : base(db)
        {
            _db = db;
        }


    }
}
