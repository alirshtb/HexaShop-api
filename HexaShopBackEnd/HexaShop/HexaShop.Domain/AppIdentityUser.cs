using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class AppIdentityUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        public string FullName => FirstName + " " + LastName;

        #region Navigations

        public virtual ICollection<AppIdentityUserRole> Roles { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

        #endregion
    }
}
