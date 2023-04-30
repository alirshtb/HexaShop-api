using HexaShop.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.ProductDtos.Queries
{
    public class GetProductListRequestDto : GetListBaseDto
    {
        public int? Score { get; set; }
        public long? Price { get; set; }
        public bool? IsSpecial { get; set; }
        public List<int> Categories { get; set; }

    }
}
