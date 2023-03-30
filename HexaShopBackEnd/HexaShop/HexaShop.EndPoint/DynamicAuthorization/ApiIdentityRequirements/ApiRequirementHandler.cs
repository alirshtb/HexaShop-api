using HexaShop.ApiEndPoint.DynamicAuthorization.Utilities;
using HexaShop.Common;
using Microsoft.AspNetCore.Authorization;

namespace HexaShop.ApiEndPoint.DynamicAuthorization.HexaIdentityRequirements
{
    public class ApiRequirementHandler : AuthorizationHandler<ApiRequirement>
    {
        private readonly IAthorizeUtilities _authorizeUtilities;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ApiRequirementHandler(IAthorizeUtilities authorizeUtilities, IHttpContextAccessor httpContextAccessor)
        {
            _authorizeUtilities = authorizeUtilities;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiRequirement requirement)
        {
            var claims = _authorizeUtilities.GetClaim(_httpContextAccessor.HttpContext);
            if (!string.IsNullOrWhiteSpace(claims))
            {
                if(context.User.HasClaim("Permmission", claims))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }

            return Task.CompletedTask;
        }
    }
}
