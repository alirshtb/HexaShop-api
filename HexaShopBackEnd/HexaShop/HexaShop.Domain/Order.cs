using HexaShop.Common;
using HexaShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class Order : BaseDomainEntity
    {
        public long Amount { get; set; } // --- before deductions --- //
        public long DiscountAmount { get; set; } // --- sum of discounts for products --- //
        public long TaxAmount { get; set; } // --- payable tax amount for Amount --- //
        public bool IsCompleted { get; set; }
        public OrderProgressLevel Level { get; set; }

        #region Relations

        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }
        
        public virtual ICollection<Payment> Payments { get; set; }
        
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<OrderLevelLog> LevelLogs { get; set; }


        public virtual ICollection<OrderDetails> Details { get; set; }

        #endregion
    }
}
