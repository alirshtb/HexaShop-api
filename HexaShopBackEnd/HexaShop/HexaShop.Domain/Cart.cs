using HexaShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class Cart : BaseDomainEntity
    {

        public Guid BrowserId { get; set; }

        #region Relations

        public virtual AppUser User { get; set; }
        public int? AppUserId { get; set; }

        public virtual ICollection<CartItems> Items { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        
        #endregion
    }
}
