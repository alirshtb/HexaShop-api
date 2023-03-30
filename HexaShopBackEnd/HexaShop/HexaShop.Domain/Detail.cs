using HexaShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class Detail : BaseDomainEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }


        #region Relations

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        #endregion Relations
    }
}
