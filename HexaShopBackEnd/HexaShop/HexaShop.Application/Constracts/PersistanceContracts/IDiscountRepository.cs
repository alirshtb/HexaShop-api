﻿using HexaShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Constracts.PersistanceContracts
{
    public interface IDiscountRepository : IGenericRepository<Discount>
    {
        bool IsDuplicate(int percent);
        IQueryable<Discount> GetAllAsQueryable(List<string> includes);
    }
}
