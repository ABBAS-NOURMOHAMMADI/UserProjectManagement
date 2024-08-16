using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UserProjectManagement.Services
{
    public static class AuthorizationExtensions
    {
        private static readonly IEnumerable<string> _scopeClaimTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase) {
        "http://schemas.microsoft.com/identity/claims/scope",
        "scope"
    };

        public static AuthorizationPolicyBuilder RequireScope(this AuthorizationPolicyBuilder builder, params string[] scopes)
        {
            return builder.RequireAssertion(context =>
                context.User
                    .Claims
                    .Where(c => _scopeClaimTypes.Contains(c.Type))
                    .SelectMany(c => c.Value.Split(' '))
                    .Any(s => scopes.Contains(s, StringComparer.Ordinal))
            );
        }

        public static bool HasAnyScope(this ClaimsPrincipal principal, params string[] scopes)
        {
            return principal
                    .Claims
                    .Where(c => _scopeClaimTypes.Contains(c.Type))
                    .SelectMany(c => c.Value.Split(' '))
                    .Any(s => scopes.Contains(s, StringComparer.Ordinal));
        }

        public static bool HasAnyPermission(this ClaimsPrincipal principal, params string[] permissions)
        {
            return principal
                    .Claims
                    .Where(c => c.Type == "permission")
                    .SelectMany(c => c.Value.Split(' '))
                    .Any(s => permissions.Contains(s, StringComparer.Ordinal))
            ;
        }
    }
}
