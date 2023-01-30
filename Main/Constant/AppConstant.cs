namespace MeadowPaymentService.Constant;

public static class AppConstant
{
    public const string BasePath = "/revice-commerce/meadow-payment-service";
    
    public static class JwtClaim
    {
        public const string Username = "preferred_username";
        
        public const string Audience = "account";
    }
    
    public static class ConnectionStringKey
    {
        public const string PostgresqlUrl = "PostgresqlUrl";

        public const string RedisUrl = "RedisUrl";
        
        public const string JwksUrl = "JwksUrl";
        
        public const string JwtIssuerUrl = "JwtIssuerUrl";
    }
    
    public static class CacheKey
    {
        private const string DomainKey = "REVICE_COMMERCE";

        private const string Separator = "::";
        
        public const string JwksKey = DomainKey + Separator + "JWKS";
    }
}