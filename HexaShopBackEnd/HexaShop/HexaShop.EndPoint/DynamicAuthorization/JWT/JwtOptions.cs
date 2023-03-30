namespace HexaShop.ApiEndPoint.DynamicAuthorization.JWT
{
    public class JwtOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationMinutes { get; set; }
    }
}
