using HexaShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class Product : BaseDomainEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public long Price { get; set; }
        public string MainImage { get; set; }
        public bool IsSpecial { get; set; }



        #region Relations 
        public virtual ICollection<Detail> Details { get; set; }
        public virtual ICollection<ImageSource> Images { get; set; }
        public virtual ICollection<ProductInCategory> Categories { get; set; }
        #endregion Relations
    }
}




