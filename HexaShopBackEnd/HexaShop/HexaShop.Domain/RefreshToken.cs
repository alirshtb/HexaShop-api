using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Domain
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Revoked { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => Revoked != null && !IsExpired;



        public string UserId { get; set; }
        public virtual AppIdentityUser User { get; set; }
    }
}
