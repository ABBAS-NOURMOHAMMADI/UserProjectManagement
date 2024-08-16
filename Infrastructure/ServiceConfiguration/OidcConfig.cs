using Microsoft.AspNetCore.Http;

namespace Infrastructure.ServiceConfiguration
{
    public class OidcConfig
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string? ResponseType { get; set; }
        public List<string> Scopes { get; set; } = new List<string>();
        public bool UsePkce { get; set; } = true;
        public bool RequireHttpsMetadata { get; set; } = false;
        public List<string> Issuers { get; set; } = new List<string>();
        public string Issuer { get; set; }
        public string RememberMeClientId { get; set; }
        public string RememberMeClientSecret { get; set; }

        public SameSiteMode NonceCookieSameSite { get; set; } = SameSiteMode.Lax;
        public CookieSecurePolicy NonceCookieSecurePolicy { get; set; } = CookieSecurePolicy.SameAsRequest;
        public SameSiteMode CorrelationCookieSameSite { get; set; } = SameSiteMode.Lax;
        public CookieSecurePolicy CorrelationCookieSecurePolicy { get; set; } = CookieSecurePolicy.SameAsRequest;
    }
}
