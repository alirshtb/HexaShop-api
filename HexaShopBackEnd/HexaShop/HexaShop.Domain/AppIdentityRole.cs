using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class AppIdentityRole : IdentityRole
    {
        public string DisplayName { get; set; }

        #region Navigations

        public virtual ICollection<AppIdentityUserRole> Users { get; set; }

        #endregion
    }
}
