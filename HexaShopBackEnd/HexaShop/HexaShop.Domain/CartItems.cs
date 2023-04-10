using HexaShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class CartItems : BaseDomainEntity
    {
        public int Count { get; set; }
        public long Price { get; set; }


        #region Relations 

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }


        public virtual Cart Cart { get; set; }
        public int CartId { get; set; }



        #endregion
    }
}
