using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common.Dtos
{
    public class GetListMetaData
    {
        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PagesCount { get; set; }
        public int RowsCount { get; set; }

    }
}
