using AutoMapper.Configuration.Conventions;
using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Persistance.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly HexaShopDbContext _dbContext;

        public CartRepository(HexaShopDbContext hexaShopDbContext) : base(hexaShopDbContext)
        {
            _dbContext = hexaShopDbContext;
        }

        /// <summary>
        /// get last active and unFinished cart of a browser
        /// </summary>
        /// <param name="browserId"></param>
        /// <param name="includes"></param>
        /// <returns>cart</returns>
        public Cart GetActiveByBrowserId(string browserId, List<string> includes = null)
        {
            var allCarts = GetAsQueryable(includes: includes);
            var lastActiveOne = allCarts.OrderByDescending(c => c.DateCreated)
                                        .SingleOrDefault(c => c.IsActive == true &&
                                                     c.BrowserId.ToString().ToLower() == browserId.ToLower() &&
                                                     c.IsFinished == false);

            return lastActiveOne;
        }

        /// <summary>
        /// get last active and unFinished cart of a user 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="includes"></param>
        /// <returns>a cart</returns>
        public Cart GetActiveByUserId(int userId, List<string> includes = null)
        {
            var allCarts = GetAsQueryable(includes: includes);
            var lastActiveOne = allCarts.OrderByDescending(c => c.DateCreated)
                                        .SingleOrDefault(c => c.IsActive == true &&
                                                     c.AppUserId == userId &&
                                                     c.IsFinished == false);

            return lastActiveOne;
        }

        /// <summary>
        /// get user all carts 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public IEnumerable<Cart> GetByUserId(int userId, List<string> includes = null)
        {
            var allCarts = GetAsQueryable(includes: includes);
            allCarts = allCarts.Where(c => c.AppUserId == userId);

            return allCarts.AsEnumerable();
        }

        /// <summary>
        /// get browser all carts
        /// </summary>
        /// <param name="browserId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<Cart> GetByBrowserId(string browserId, List<string> includes = null)
        {
            var allCarts = GetAsQueryable(includes: includes);
            allCarts = allCarts.Where(c => c.BrowserId.ToString() == browserId);

            return allCarts.AsEnumerable();
        }

        /// <summary>
        /// update cart and set app user id in the cart on sign in.
        /// </summary>
        /// <param name="browserId"></param>
        /// <param name="appUserId"></param>
        public void UpdateOnSignIn(string browserId, int appUserId)
        {
            var cart = GetActiveByBrowserId(browserId);
            cart.AppUserId = appUserId;
            Update(cart);
        }

    }
}
