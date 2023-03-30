using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Domain;



namespace HexaShop.Persistance.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {

        private readonly HexaShopDbContext _dbContext;

        public CategoryRepository(HexaShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Category>> GetParents()
        {
            var parents = _dbContext.Categories.Where(c => c.ParentCategoryId == null);
            return parents;
        }

    }
}
