using HexaShop.Common;
using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Constracts.PersistanceContracts
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task ChagneOrderLevel(int id, OrderProgressLevel nextLevel, string title);
    }
}
