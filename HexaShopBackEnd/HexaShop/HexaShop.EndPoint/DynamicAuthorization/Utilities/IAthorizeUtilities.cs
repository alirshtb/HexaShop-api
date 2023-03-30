using System.Collections.Immutable;

namespace HexaShop.ApiEndPoint.DynamicAuthorization.Utilities
{
    public interface IAthorizeUtilities
    {
        public ImmutableHashSet<AuthorizedItems> AuthorizedItemsInfo { get; }

        public string GetClaim(HttpContext context);
    }
}
