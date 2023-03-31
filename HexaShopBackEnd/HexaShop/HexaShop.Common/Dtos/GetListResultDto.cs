using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Common.Dtos
{
    public class GetListResultDto<T>
    {
        public IEnumerable<T> Values { get; set; }
        public GetListMetaData MetaData { get; set; }
    }
}
