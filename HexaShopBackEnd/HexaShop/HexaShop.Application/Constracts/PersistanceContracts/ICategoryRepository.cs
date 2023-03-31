using HexaShop.Application.Dtos.CategoryDtos.Queries;
using HexaShop.Application.Dtos.Common;
using HexaShop.Common.Dtos;
using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Constracts.PersistanceContracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        IQueryable<Category> GetParents(List<string> includes);
    }
}
