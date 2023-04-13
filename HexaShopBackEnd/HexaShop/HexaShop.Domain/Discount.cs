using HexaShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class Discount : BaseDomainEntity
    {
        public string Title { get; set; }
        public string Description { get; set; } 
        public int Percent { get; set; }

        #region Relations 

        public virtual ICollection<Product> Products { get; set; }  

        #endregion
    }
}
