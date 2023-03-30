using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class ProductInCategory
    {

        public int Id { get; set; }

        #region Relations 

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        #endregion Relations 

    }
}
