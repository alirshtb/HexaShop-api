using HexaShop.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class Payment : BaseDomainEntity
    {

        public long Amount { get; set; }
        public string Info { get; set; }
        public bool IsSuccessful { get; set; }
        public string FailureReason { get; set; }
        


        #region Relations

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
            

        #endregion
    }
}
