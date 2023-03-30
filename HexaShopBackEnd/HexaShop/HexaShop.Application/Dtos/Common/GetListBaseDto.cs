using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Application.Dtos.Common
{
    public abstract class GetListBaseDto
    {
        public string Search { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
