using HexaShop.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.ProductDtos.Queries
{
    public class GetProductListRequest : GetListBaseDto
    {
        public int? Score { get; set; }
        public long? Price { get; set; }
    }
}
