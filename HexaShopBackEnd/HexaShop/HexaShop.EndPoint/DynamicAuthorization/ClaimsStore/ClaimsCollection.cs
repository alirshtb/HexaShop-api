using System.Security.Claims;

namespace HexaShop.ApiEndPoint.DynamicAuthorization.ClaimsStore
{
    public record ClaimsCollection
    {
        public string Title { get; init; }
        public IEnumerable<ApiClaimValue> Values { get; set; }
        public IEnumerable<Claim> Claims => GetClaims();
        private IEnumerable<Claim> GetClaims()
        {
            foreach (var claimValue in Values.Where(cv => cv.IsSelected == true))
            {
                yield return new Claim(ApiClaimType.UserAccess, claimValue.Name);
            }
        }
    }
}
