using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Constracts.PersistanceContracts
{
    public interface IAppUserRepository : IGenericRepository<AppUser>
    {
        Task<AppUser> GetAsync(string Email);
    }
}
