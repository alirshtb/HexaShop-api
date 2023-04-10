using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Persistance.Repositories
{
    public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        private readonly HexaShopDbContext _dbContext;

        public AppUserRepository(HexaShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// get app user by email.
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public Task<AppUser> GetAsync(string email)
        {
            var appUser = _dbContext.AppUsers
                                    .Where(au => au.Email == email)
                                    .FirstOrDefaultAsync();

            return appUser;
        }

        /// <summary>
        /// get sub id if user id authenticated.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<int?> GetCurrentUser(ClaimsPrincipal user)
        {
            if(!user.Identity.IsAuthenticated)
            {
                return null;
            }

            var email = user.Claims?.FirstOrDefault(c => c.Type.Contains(JwtRegisteredClaimNames.Email)).Value;

            var appUser = await GetAsync(email);

            return appUser == null ? null : appUser.Id;
        }
    }
}
