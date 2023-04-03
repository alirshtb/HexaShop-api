using HexaShop.Domain;

namespace HexaShop.Application.Constracts.PersistanceContracts
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable<Product> GetQueryableProducts(List<string> includes);
        Task RemoveFromCategory(int categoryId, int productId);
        Task AddToCategory(int productId, int categoryId);
        Task DeletetImages(int productId);
        Task AddImages(List<ImageSource> images, int productId);
        IQueryable<Product> GetLatestActivesQueryable(List<string> includes);
    }
}
