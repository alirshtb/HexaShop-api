using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public long UnitPrice { get; set; }
        public long UnitDiscount { get; set; }
        public long TotalDiscount { get; set; }
        public long TotalAmount { get; set; }

        #region Relations 

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public virtual Order Order { get; set; }
        public int OrderId { get; set; }

        #endregion

    }
}
