using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Domain;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// get products as queryable.
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IQueryable<Product> GetQueryableProducts(List<string> includes)
        {
            var result = GetAsQueryable(includes: includes);
            return result;
        }

        /// <summary>
        /// add product to a category.
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task AddToCategory(int productId, int categoryId)
        {
            var includes = new List<string>()
            {
                "Categories"
            };
            var product = await GetAsync(productId, includes);

            var productInCategory = new ProductInCategory()
            {
                ProductId = productId,
                CategoryId = categoryId
            };

            product.Categories.Add(productInCategory);

            await _dbContext.SaveChangesAsync();

            await Task.CompletedTask;

        }

        /// <summary>
        /// remove product from a category.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task RemoveFromCategory(int categoryId, int productId)
        {
            var includes = new List<string>()
            {
                "Categories"
            };
            var product = await GetAsync(productId, includes);

            var productInCategory = product.Categories.Where(pic => pic.CategoryId == categoryId).FirstOrDefault();

            product.Categories.Remove(productInCategory);

            await _dbContext.SaveChangesAsync();

            await Task.CompletedTask;

        }

        /// <summary>
        /// remove a product images.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task DeletetImages(int productId)
        {
            var product = await GetAsync(productId, includes: new List<string>()
            {
                "Images"
            });
            product.Images.Clear();
            await _dbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        /// <summary>
        /// add images to product.
        /// </summary>
        /// <param name="images"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task AddImages(List<ImageSource> images, int productId)
        {
            var product = await GetAsync(productId, includes: new List<string>()
            {
                "Images"
            });

            images.ForEach(img =>
            {
                product.Images.Add(img);
            });

            await _dbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }

        /// <summary>
        /// select 
        /// </summary>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IQueryable<Product> GetLatestActivesQueryable(List<string> includes)
        {
            var result = GetAsQueryable(includes).Where(p => p.IsActive == true).OrderByDescending(p => p.DateCreated);
            return result;
        }
    }
}
