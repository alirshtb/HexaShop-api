using HexaShop.ApiEndPoint.DynamicAuthorization.Attributes;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Immutable;
using System.Reflection;

namespace HexaShop.ApiEndPoint.DynamicAuthorization.Utilities
{
    public class AuthorizeUtilities : IAthorizeUtilities
    {
        public ImmutableHashSet<AuthorizedItems> AuthorizedItemsInfo { get; }

        public AuthorizeUtilities(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            var authorizedItems = new List<AuthorizedItems>();
            var actionDescriptors = actionDescriptorCollectionProvider.ActionDescriptors.Items;

            foreach (var actionDescriptor in actionDescriptors)
            {
                if(!(actionDescriptor is ControllerActionDescriptor descriptor))
                {
                    continue;
                }

                //var controllerTypeInfo = descriptor.ControllerTypeInfo; // --- this is for finding the area --- //

                var claimValues = descriptor.MethodInfo.GetCustomAttribute<ApiAuthoizationAttribute>()?.AuthorizedClaimValue;

                if(!string.IsNullOrWhiteSpace(claimValues))
                {
                    authorizedItems.Add(new AuthorizedItems(descriptor.ControllerName, descriptor.ActionName, claimValues));
                }

                AuthorizedItemsInfo = ImmutableHashSet.CreateRange(authorizedItems);

            }


        }


        public string GetClaim(HttpContext context)
        {
            var actionName = context.GetRouteValue("action")?.ToString();
            var controllerName = context.GetRouteValue("controller")?.ToString();

            AuthorizedItemsInfo.TryGetValue(new AuthorizedItems(controllerName, actionName, string.Empty), out var model);

            return model?.AuthorizedClaimValue;

        }
    }
}
