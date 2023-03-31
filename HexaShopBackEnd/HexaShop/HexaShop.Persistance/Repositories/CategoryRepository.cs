using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Application.Dtos.CategoryDtos.Queries;
using HexaShop.Common.CommonExtenstionMethods;
using HexaShop.Common.Dtos;
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
        /// get paginated parents
        /// </summary>
        /// <param name="request"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IQueryable<Category> GetParents(List<string> includes)
        {
            var parentCategories = GetAsQueryable(includes: includes).Where(c => c.ParentCategoryId == null);
            return parentCategories.AsQueryable();
            
        }
    }
}
