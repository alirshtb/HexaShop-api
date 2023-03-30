using HexaShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class ImageSource : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }


        #region Relations 

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        

        #endregion Relations 
    }
}
