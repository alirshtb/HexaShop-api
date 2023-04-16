using HexaShop.Application.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.DiscountDtos
{
    public class GetDiscountListRequestDto : GetListBaseDto
    {
        public int? Percent { get; set; }
    }
}
