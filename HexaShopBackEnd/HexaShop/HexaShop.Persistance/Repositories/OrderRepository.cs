using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Common;
using HexaShop.Common.CommonExtenstionMethods;
using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Persistance.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly HexaShopDbContext _dbContext;

        public OrderRepository(HexaShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// change order level
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nextLevel"></param>
        /// <param name="title"></param>
        public async Task ChagneOrderLevel(int id, OrderProgressLevel nextLevel, string title)
        {
            var orderIncludes = new List<string>()
            {
                "LevelLogs"
            };
            var order = await GetAsync(id, includes: orderIncludes);

            if (order == null)
            {
                ExceptionHelpers.ThrowException(ApplicationMessages.OrderNotFound);
            }

            var level = order.LevelLogs.OrderByDescending(lg => lg.Id).First().NextLevel;

            order.Level = level;

            order.LevelLogs.Add(new OrderLevelLog()
            {
                CurrentLevel = order.Level,
                NextLevel = nextLevel,
                Title = title,
            });

            await UpdateAsync(order);

            await Task.CompletedTask;
        }

    }
}
