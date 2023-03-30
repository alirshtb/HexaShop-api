using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class AppIdentityUserRole : IdentityUserRole<string>
    {
        public virtual AppIdentityUser User { get; set; }
        public virtual AppIdentityRole Role { get; set; }
    }
}
