using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace HexaShop.ApiEndPoint.DynamicAuthorization.Attributes
{
    public class ApiAuthoizationAttribute : AuthorizeAttribute
    {
        public string AuthorizedClaimValue { get; set; }
        public ApiAuthoizationAttribute(string authorizedClaimValue) : base(ApiAuthorizationConstants.HexaPolicy)
        {
            AuthorizedClaimValue = authorizedClaimValue;
        }


    }
}
