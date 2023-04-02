using HexaShop.Domain;

namespace HexaShop.Application.Constracts.PersistanceContracts
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable<Product> GetQueryableProducts(List<string> includes);
        Task RemoveFromCategory(int categoryId, int productId);
        Task AddToCategory(int productId, int categoryId);
        Task DeleteProductImages(int productId);
        Task AddImagesToProduct(List<ImageSource> images, int productId);
    }
}
