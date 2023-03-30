using HexaShop.ApiEndPoint.DynamicAuthorization.ClaimsStore;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace HexaShop.EndPoint.Models.ViewModels.AdminController
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<ClaimsCollection> Claims { get; set; }
    }
}
