using HexaShop.Domain;

namespace HexaShop.Application.Constracts.PersistanceContracts
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IQueryable<Product> GetQueryableProducts(List<string> includes);
    }
}
