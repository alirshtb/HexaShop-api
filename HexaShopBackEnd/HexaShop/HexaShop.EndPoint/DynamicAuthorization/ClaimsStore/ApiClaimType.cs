using HexaShop.ApiEndPoint.DynamicAuthorization.ClaimsStore.HexaShopClaims;
using System.Collections.Immutable;
using System.Security.Claims;

namespace HexaShop.ApiEndPoint.DynamicAuthorization.ClaimsStore
{
    public class ApiClaimType
    {
        public const string UserAccess = nameof(UserAccess);
        public static ImmutableHashSet<ClaimsCollection> ClaimsCollection =>
                ImmutableHashSet.CreateRange(new List<ClaimsCollection>()
                {
                    new()
                    {
                        Title = "محصولات",
                        Values = new List<ApiClaimValue>()
                        {
                            new()
                            {
                                Name = ProductClaims.GetList,
                                DisplayName = ApiClaimsPersianValues.GetList,
                                IsSelected = false
                            },
                            new()
                            {
                                Name = ProductClaims.Create,
                                DisplayName = ApiClaimsPersianValues.Create,
                                IsSelected = false
                            },
                            new()
                            {
                                Name = ProductClaims.Update,
                                DisplayName = ApiClaimsPersianValues.Update,
                                IsSelected = false
                            },
                            new()
                            {
                                Name = ProductClaims.Delete,
                                DisplayName = ApiClaimsPersianValues.Delete,
                                IsSelected = false
                            },
                            new()
                            {
                                Name = ProductClaims.Get,
                                DisplayName = ApiClaimsPersianValues.Get,
                                IsSelected = false
                            }
                        }
                    },
                    new()
                    {
                        Title = "سفارشات",
                        Values = new List<ApiClaimValue>()
                        {
                            new()
                            {
                                Name = OrderClaims.GetList,
                                DisplayName = ApiClaimsPersianValues.GetList,
                                IsSelected = false
                            },
                            new()
                            {
                                Name = OrderClaims.Create,
                                DisplayName = ApiClaimsPersianValues.Create,
                                IsSelected = false
                            },
                            new()
                            {
                                Name = OrderClaims.Update,
                                DisplayName = ApiClaimsPersianValues.Update,
                                IsSelected = false
                            },
                            new()
                            {
                                Name = OrderClaims.Delete,
                                DisplayName = ApiClaimsPersianValues.Delete,
                                IsSelected = false
                            },
                            new()
                            {
                                Name = OrderClaims.Get,
                                DisplayName = ApiClaimsPersianValues.Get,
                                IsSelected = false
                            },
                            new()
                            {
                                Name = OrderClaims.Reject,
                                DisplayName = ApiClaimsPersianValues.Reject,
                                IsSelected = false
                            },
                            new()
                            {
                                Name = OrderClaims.Confirm,
                                DisplayName = ApiClaimsPersianValues.Confirm,
                                IsSelected = false
                            }
                        }
                    }
                });
        public static IEnumerable<ClaimsCollection> ConvertToClaimsCollection(IEnumerable<Claim> claims)
        {
            var allClaims = ClaimsCollection.ToList();
            var allClaimsValues = claims.Select(c => c.Value).ToList();
            allClaims.SelectMany(c => c.Values).Where(cv => allClaimsValues.Contains(cv.Name)).ToList().ForEach(cc => cc.IsSelected = true);
            return allClaims;
        }
    }
}
