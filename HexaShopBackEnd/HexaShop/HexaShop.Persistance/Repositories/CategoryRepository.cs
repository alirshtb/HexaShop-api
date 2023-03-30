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

        /// <summary>
        /// get parent categories as queryable.
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IQueryable<Category> GetParents(List<string> includes)
        {
            var parents = GetAsQueryable(includes: includes).Where(c => c.ParentCategoryId == null)
                                                            .AsQueryable();
            return parents;
        }

    }
}
