using HexaShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class OrderLevelLog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public OrderProgressLevel CurrentLevel { get; set; }
        public OrderProgressLevel NextLevel { get; set; }


        #region Relations 

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        #endregion
    }
}
