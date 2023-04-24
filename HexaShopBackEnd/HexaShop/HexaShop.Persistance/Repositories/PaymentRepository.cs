using AutoMapper;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Common.CommonDtos;
using HexaShop.Common.CommonExtenstionMethods;
using HexaShop.Domain;

namespace HexaShop.Persistance.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {

        private readonly HexaShopDbContext _dbContext;

        public PaymentRepository(HexaShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
