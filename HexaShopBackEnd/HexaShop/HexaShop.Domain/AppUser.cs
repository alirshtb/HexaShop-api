
using HexaShop.Common;
using HexaShop.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HexaShop.Domain
{

    public class AppUser : BaseDomainEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Mobile { get; set; }
        public string AppIdentityUserId { get; set; }


    }
}
