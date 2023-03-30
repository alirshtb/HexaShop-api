using HexaShop.Application.Constracts.PersistanceContracts;
using HexaShop.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <exception cref="NotImplementedException"></exception>
        public Task<AppUser> GetAsync(string email)
        {
            var appUser = _dbContext.AppUsers
                                    .Where(au => au.Email == email)
                                    .FirstOrDefaultAsync();

            return appUser;
        }
    }
}
