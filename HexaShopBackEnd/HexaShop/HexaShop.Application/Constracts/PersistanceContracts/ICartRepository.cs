using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Constracts.PersistanceContracts
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        IEnumerable<Cart> GetByUserId(int userId, List<string> includes = null);
        IEnumerable<Cart> GetByBrowserId(string browserId, List<string> includes = null);
        Cart GetActiveByUserId(int userId, List<string> includes = null);
        Cart GetActiveByBrowserId(string browserId, List<string> includes = null);
        void UpdateOnSignIn(string browserId, int appUserId);
        Order GetNotCompletedOrder(int cartId);
    }
}
