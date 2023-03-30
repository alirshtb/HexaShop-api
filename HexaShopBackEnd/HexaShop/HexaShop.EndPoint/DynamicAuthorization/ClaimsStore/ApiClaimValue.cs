namespace HexaShop.ApiEndPoint.DynamicAuthorization.ClaimsStore
{
    public record ApiClaimValue
    {
        public string Name { get; init; }
        public string DisplayName { get; init; }
        public bool IsSelected { get; set; }

    }
}
