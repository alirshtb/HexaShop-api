using System.Drawing;

namespace HexaShop.ApiEndPoint.DynamicAuthorization.ClaimsStore.HexaShopClaims
{
    public static class OrderClaims
    {
        public const string Get = nameof(Get) + nameof(OrderClaims);
        public const string GetList = nameof(GetList) + nameof(OrderClaims);
        public const string Create = nameof(Create) + nameof(OrderClaims);
        public const string Delete = nameof(Delete) + nameof(OrderClaims);
        public const string Update = nameof(Update) + nameof(OrderClaims);
        public const string Confirm = nameof(Confirm) + nameof(OrderClaims);
        public const string Reject = nameof(Reject) + nameof(OrderClaims);
    }
}
